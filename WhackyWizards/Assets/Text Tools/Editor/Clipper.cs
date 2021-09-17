// Copyright (C) 2019 Alexander Klochkov - All Rights Reserved
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms


using System;


namespace texttools
{
	public enum ClipOp
	{
		UNION        = 0,
		INTERSECTION = 1,
	}

	internal enum BundleState
	{
		UNBUNDLED = 0,
		PARENT    = 1,
		CHILD     = 2
	}

	internal class Node
	{
		internal Node  next;
		internal float x;
		internal float y;
		internal bool  join;

		internal Node(float x, float y, bool join)
		{
			this.next = null;
			this.x    = x;
			this.y    = y;
			this.join = join;
		}
	}

	internal class Out
	{
		internal Node head;
		internal Node tail;
		internal Node start;

		internal int  count;

		internal Out(float x, float y, bool join)
		{
			start = head = tail = new Node(x, y, join);
			count = 1;
		}

		internal void PushFront(Out o)
		{
			o.tail.next = head;
			head = o.head;
			o.tail = tail;
			count += o.count;
			o.count = count;

			if(o.start.y.CompareTo(start.y) > 0)
				start = o.start;
			else
				o.start = start;
		}

		internal void PushFront(float x, float y, bool join)
		{
			Node n = new Node(x, y, join);

			n.next = head;
			head   = n;
			++count;
		}

		internal void PushBack(float x, float y, bool join)
		{
			Node n = new Node(x, y, join);

			tail.next = n;
			tail = n;
			++count;
		}

		internal Point[] ToArray()
		{
			Node n = head;
			count  = 0;

			while(null != n)
			{
				++count;
				n = n.next;
			}

			Point[] point = new Point[count];
			n = head;

			for(int i=0; i<count; ++i)
			{
				point[i].x    = n.x;
				point[i].y    = n.y;
				point[i].join = n.join;
				n = n.next;
			}

			return point;
		}
	}

	internal class ClipperBound
	{
		internal ClipperBound nextLocalMinima;
		internal ClipperBound nextActiveEdge;
		internal ClipperBound prevActiveEdge;
		internal ClipperBound nextSortedEdge;

		internal int          begin;
		internal int          end;
		internal int          upper;
		internal int          lower;
		internal int          direction;

		internal float        topX;
		internal float        bottomX;
		internal float        dx;

		internal int          wc1;
		internal int          wc2;
		internal int          bottomLeftFill;
		internal int          bottomRightFill;

		internal int          isClip;
		internal BundleState  bundle;

		internal Point[]      inPointList;
		internal Out          outList;

		internal float Top
		{
			get { return inPointList[end].y; }
		}

		internal float Bottom
		{
			get { return inPointList[begin].y; }
		}

		internal float EdgeTop
		{
			get { return inPointList[upper].y; }
		}

		internal float EdgeBottom
		{
			get { return inPointList[lower].y; }
		}

		internal bool Join
		{
			get { return inPointList[lower].join; }
		}

		internal ClipperBound(Point[] points, int begin, int end, int direction, int clip)
		{
			this.nextLocalMinima = null;
			this.nextActiveEdge  = null;
			this.prevActiveEdge  = null;
			this.nextSortedEdge  = null;

			this.begin           = begin;
			this.end             = end;
			this.upper           = (begin + direction + points.Length) % points.Length;
			this.lower           = begin;
			this.direction       = direction;


			this.topX            = points[begin].x;
			this.bottomX         = points[begin].x;
			this.dx              = (points[upper].x - points[lower].x) / (points[upper].y - points[lower].y);

			this.wc1             = 0;
			this.wc2             = 0;
			this.bottomLeftFill  = 0;
			this.bottomRightFill = 0;

			this.isClip          = clip;
			this.bundle          = BundleState.UNBUNDLED;
			this.inPointList     = points;
			this.outList         = null;
		}

		internal void SetTopX(float top)
		{
			topX = (top.Equals(EdgeTop)) ? inPointList[upper].x : inPointList[lower].x + dx * (top - EdgeBottom);
		}

		internal bool Update(float top)
		{
			bottomX        = topX;
			nextSortedEdge = null;
			bundle         = BundleState.UNBUNDLED;

			if(upper != end && top.Equals(EdgeTop))
			{
				lower   = upper;
				upper   = (upper + direction + inPointList.Length) % inPointList.Length;
				dx      = (inPointList[upper].x - inPointList[lower].x) / (inPointList[upper].y - inPointList[lower].y);
				bottomX = inPointList[lower].x;

				return true;
			}

			if(top.Equals(Top))
				lower = upper;

			return false;
		}
	}

	internal class Intersection
	{
		internal ClipperBound a;
		internal ClipperBound b;

		internal float        x;
		internal float        y;
	}

	public static class Clipper
	{
		static ClipperBound   localMinimaList  = null;
		static ClipperBound   activeEdgeList   = null;
		static ClipperBound   sortedEdgeList   = null;

		static float[]        scanBeam         = new float[4096];
		static int            numScanBeams     = 0;

		static Intersection[] intersection     = new Intersection[4096];
		static int            numIntersections = 0;

		static FillRule[]     fillRule         = new FillRule[2];
		static int[]          windingCount     = new int[2];

		static Path           outPath          = null;

		internal static float epsilon          = 0.0001f;
		
		static void Clear()
		{
			ClipperBound lm;

			while(null != localMinimaList)
			{
				lm = localMinimaList.nextLocalMinima;
				localMinimaList.nextLocalMinima = null;
				localMinimaList.nextActiveEdge  = null;
				localMinimaList.prevActiveEdge  = null;
				localMinimaList.nextSortedEdge  = null;
				localMinimaList.outList         = null;
				localMinimaList.inPointList     = null;
				localMinimaList = null;
				localMinimaList = lm;
			}

			localMinimaList  = null;
			activeEdgeList   = null;
			sortedEdgeList   = null;
			outPath          = null;
			numScanBeams     = 0;
			numIntersections = 0;
		}

		static void AddScanBeam(float sb)
		{
			for(int i=numScanBeams-1; i>=0; --i)
			{
				if(sb.Equals(scanBeam[i]))
					return;
			}

			scanBeam[numScanBeams++] = sb;

			for(int i=numScanBeams-2; i>=0; --i)
			{
				if(sb.CompareTo(scanBeam[i]) > 0)
					break;

				scanBeam[i+1] = scanBeam[i];
				scanBeam[i]   = sb;
			}
		}

		static void AddLocalMinima(ClipperBound lm)
		{
			ClipperBound prev = null;
			ClipperBound curr = localMinimaList;

			while(null != curr && curr.Bottom.CompareTo(lm.Bottom) > 0)
			{
				prev = curr;
				curr = curr.nextLocalMinima;
			}

			if(null == prev)
			{
				lm.nextLocalMinima = curr;
				localMinimaList    = lm;
			}
			else
			{
				prev.nextLocalMinima = lm;
				lm.nextLocalMinima   = curr;
			}
		}

		static void AddActiveEdge(ClipperBound ae)
		{
			ClipperBound prev = null;
			ClipperBound curr = activeEdgeList;

			while(null != curr && (ae.bottomX.CompareTo(curr.bottomX) > 0 || (ae.bottomX.Equals(curr.bottomX) && ae.dx.CompareTo(curr.dx) < 0)))
			{
				prev = curr;
				curr = curr.nextActiveEdge;
			}

			if(null == prev)
			{
				ae.nextActiveEdge = curr;
				activeEdgeList    = ae;
			}
			else
			{
				prev.nextActiveEdge = ae;
				ae.prevActiveEdge   = prev;
				ae.nextActiveEdge   = curr;
			}

			if(null != curr)
				curr.prevActiveEdge = ae;
		}

		static ClipperBound RemoveActiveEdge(ClipperBound ae)
		{
			ClipperBound prev = ae.prevActiveEdge;
			ClipperBound next = ae.nextActiveEdge;

			if(null != prev)
			{
				prev.nextActiveEdge = next;
				if(null != next)
					next.prevActiveEdge = prev;
			}
			else
			{
				activeEdgeList = next;
				if(null != activeEdgeList)
					activeEdgeList.prevActiveEdge = null;
			}

			ae.prevActiveEdge = ae.nextActiveEdge = null;

			return next;
		}

		static void SwapActiveEdges(ClipperBound a, ClipperBound b)
		{
			ClipperBound prev = a.prevActiveEdge;
			ClipperBound next = b.nextActiveEdge;

			if(null != next)
				next.prevActiveEdge = a;

			while(null != prev && prev.bundle == BundleState.CHILD)
				prev = prev.prevActiveEdge;

			if(null == prev)
			{
				activeEdgeList.prevActiveEdge = b;
				b.nextActiveEdge = activeEdgeList;
				activeEdgeList = a.nextActiveEdge;
			}
			else
			{
				prev.nextActiveEdge.prevActiveEdge = b;
				b.nextActiveEdge = prev.nextActiveEdge;
				prev.nextActiveEdge = a.nextActiveEdge;
			}

			a.nextActiveEdge.prevActiveEdge = prev;
			b.nextActiveEdge.prevActiveEdge = b;
			a.nextActiveEdge = next;
		}

		static void AppendPolygon(Out a, Out b, bool left)
		{
			ClipperBound e = activeEdgeList;

			if(a == b)
			{
				SubPath sp = new SubPath();
				sp.points = a.ToArray();
				outPath.subPathList.Add(sp);
			}
			else
			{
				a.PushFront(b);

				if(left)
				{
					while(null != e)
					{
						if(b == e.outList)
							e.outList = a;
						e = e.nextActiveEdge;
					}
				}
				else
				{
					while(null != e)
					{
						if(a == e.outList)
							e.outList = b;
						e = e.nextActiveEdge;
					}
				}
			}
		}

		static void CreateLocalMinimaList(Path path, int clip)
		{
			int count, prev, next, max, n;

			if(null == path)
				return;

			fillRule[clip] = path.fillRule;

			foreach(SubPath sp in path.subPathList)
			{
				count = sp.points.Length;

				for(int i=0; i<count; ++i)
				{
					n    = 0;
					prev = (i - 1 + count) % count;
					next = (i + 1) % count;

					if(sp.points[i].y.CompareTo(sp.points[next].y) > 0 && sp.points[prev].y.CompareTo(sp.points[i].y) <= 0)
					{
						do
						{
							max  = next;
							next = (max + 1) % count;
							++n;
						}
						while(sp.points[max].y.CompareTo(sp.points[next].y) > 0);

						AddLocalMinima(new ClipperBound(sp.points, i, max, +1, clip));
						AddScanBeam(sp.points[i].y);
					}

					next = (i + 1) % count;

					if(sp.points[i].y.CompareTo(sp.points[prev].y) > 0 && sp.points[next].y.CompareTo(sp.points[i].y) <= 0)
					{
						do
						{
							max  = prev;
							prev = (max - 1 + count) % count;
						}
						while(sp.points[max].y.CompareTo(sp.points[prev].y) > 0);

						AddLocalMinima(new ClipperBound(sp.points, i, max, -1, clip));
						AddScanBeam(sp.points[i].y);
					}

					i += n;
				}
			}
		}

		static void AddIntersection(ClipperBound a, ClipperBound b, float top, float bottom)
		{
			Intersection i = new Intersection();

			float r = ((b.inPointList[b.upper].x - b.inPointList[b.lower].x)*(a.EdgeBottom - b.EdgeBottom) - (b.EdgeTop - b.EdgeBottom)*(a.inPointList[a.lower].x - b.inPointList[b.lower].x)) /
                      ((b.EdgeTop - b.EdgeBottom)*(a.inPointList[a.upper].x - a.inPointList[a.lower].x) - (b.inPointList[b.upper].x - b.inPointList[b.lower].x)*(a.EdgeTop - a.EdgeBottom));

			i.a = a;
			i.b = b;
			i.x = a.inPointList[a.lower].x + r * (a.inPointList[a.upper].x - a.inPointList[a.lower].x);
			i.y = r * (a.EdgeTop - a.EdgeBottom) + a.EdgeBottom;

			intersection[numIntersections++] = i;

			for(int j=numIntersections-2; j>=0; --j)
			{
				if(i.y.CompareTo(intersection[j].y) <= 0)
					break;

				intersection[j+1] = intersection[j];
				intersection[j]   = i;
			}
		}

		static void CreateIntersections(float top, float bottom)
		{
			if(null == activeEdgeList)
				return;

			ClipperBound prev, se, ae = activeEdgeList;

			while(null != ae && ae.bundle == BundleState.CHILD)
				ae = ae.nextActiveEdge;

			sortedEdgeList = ae;
			ae = ae.nextActiveEdge;

			while(null != ae)
			{
				if(ae.bundle == BundleState.CHILD)
				{
					ae = ae.nextActiveEdge;
					continue;
				}

				se = sortedEdgeList;
				prev = null;

				while(null != se && ae.topX.CompareTo(se.topX) < 0)
				{
					AddIntersection(se, ae, top, bottom);

					prev = se;
					se = se.nextSortedEdge;
				}

				if(null == se)
				{
					prev.nextSortedEdge = ae;
				}
				else
				{
					ae.nextSortedEdge = se;
					if(null != prev)
						prev.nextSortedEdge = ae;
					else
						sortedEdgeList = ae;
				}

				ae = ae.nextActiveEdge;
			}
		}

		static int IntersectionType(ClipOp op, ClipperBound a, ClipperBound b)
		{
			ClipperBound e;
			int   left, top, right, bottom;

			left   = a.bottomLeftFill;
			right  = b.bottomRightFill;
			bottom = a.bottomRightFill;

			e = a;
			windingCount[b.isClip]   = b.wc1;
			windingCount[b.isClip^1] = b.wc2;

			do
			{
				windingCount[e.isClip] = (fillRule[e.isClip] == FillRule.EVEN_ODD) ? windingCount[e.isClip] ^ 1 : windingCount[e.isClip] - e.direction;
				e = e.prevActiveEdge;
			}
			while(null != e && e.bundle == BundleState.CHILD);

			a.wc1 = b.wc1;
			a.wc2 = b.wc2;
			b.wc1 = windingCount[b.isClip];
			b.wc2 = windingCount[b.isClip^1];

			top = (b.wc1 == 0) ? 0 : 1;

			if(ClipOp.UNION == op)
				top = (0 != b.wc2) ? 1 : top;
			else if(ClipOp.INTERSECTION == op)
				top = (0 == b.wc2) ? 0 : top;

			a.bottomLeftFill  = top;
			a.bottomRightFill = right;
			b.bottomLeftFill  = left;
			b.bottomRightFill = top;

			e = a.prevActiveEdge;
			while(null != e && e.bundle == BundleState.CHILD)
			{
				e.bottomLeftFill = e.bottomRightFill = a.bottomLeftFill;
				e = e.prevActiveEdge;
			}

			e = b.prevActiveEdge;
			while(null != e && e.bundle == BundleState.CHILD)
			{
				e.bottomLeftFill = e.bottomRightFill = b.bottomLeftFill;
				e = e.prevActiveEdge;
			}

			return (left<<12)|(top<<8)|(right<<4)|(bottom);
		}

		static void ProcessIntersections(ClipOp op)
		{
			ClipperBound a, b;
			Out          temp;
			int          type;
			float        x, y;

			for(int i=0; i<numIntersections; ++i)
			{
				a    = intersection[i].a;
				b    = intersection[i].b;
				x    = intersection[i].x;
				y    = intersection[i].y;
				type = IntersectionType(op, a, b);

				switch(type)
				{
					case 0x0001:
						if(null != b.outList)
							b.outList.PushFront(x, y, true);
						if(null != a.outList && null != b.outList)
							AppendPolygon(b.outList, a.outList, false);
						break;
					case 0x0010:
						if(null != b.outList)
							b.outList.PushBack(x, y, true);
						a.outList = b.outList;
						break;
					case 0x0100:
						a.outList = b.outList = new Out(x, y, true);
						break;
					case 0x0101:
						if(null != a.outList)
							a.outList.PushBack(x, y, true);
						if(null != b.outList)
							b.outList.PushFront(x, y, true);
						temp = a.outList;
						a.outList = b.outList;
						b.outList = temp;
						break;
					case 0x0111:
						if(null != a.outList)
							a.outList.PushBack(x, y, true);
						intersection[i].b.outList = intersection[i].a.outList;
						break;
					case 0x1000:
						if(null != a.outList)
							a.outList.PushFront(x, y, true);
						b.outList = a.outList;
						break;
					case 0x1010:
						if(null != a.outList)
							a.outList.PushFront(x, y, true);
						if(null != b.outList)
							b.outList.PushBack(x, y, true);
						temp = a.outList;
						a.outList = b.outList;
						b.outList = temp;
						break;
					case 0x1011:
						a.outList = b.outList = new Out(x, y, true);
						break;
					case 0x1101:
						if(null != b.outList)
							b.outList.PushFront(x, y, true);
						a.outList = b.outList;
						break;
					case 0x1110:
						if(null != a.outList)
							a.outList.PushFront(x, y, true);
						if(null != a.outList && null != b.outList)
							AppendPolygon(a.outList, b.outList, true);
						break;
					default:
						break;
				}

				SwapActiveEdges(a, b);

				intersection[i] = null;
			}

			numIntersections = 0;
		}

		static void ProcessActiveEdges(ClipOp op, float top, float bottom)
		{
			ClipperBound ae             = activeEdgeList;
			ClipperBound ne             = null;
			Out          prevOut        = null;
			Out          tempOut        = null;
			int          topLeftFill    = 0;
			int          topRightFill   = 0;
			int          prevTopFill    = 0;
			int          prevBottomFill = 0;
			int          type           = 0;
			float        prevX          = float.MinValue;
			windingCount[0]             = 0;
			windingCount[1]             = 0;

			while(null != ae)
			{
				if(bottom.Equals(ae.Top))
				{
					topLeftFill = topRightFill = prevTopFill;
					ae.wc2      = windingCount[ae.isClip^1];
				}
				else
				{
					topLeftFill             = (windingCount[ae.isClip] == 0) ? 0 : 1;
					windingCount[ae.isClip] = (FillRule.EVEN_ODD == fillRule[ae.isClip]) ? windingCount[ae.isClip] ^ 1 : windingCount[ae.isClip] + ae.direction;
					ae.wc1                  = windingCount[ae.isClip];
					ae.wc2                  = windingCount[ae.isClip^1];
					topRightFill            = (ae.wc1 == 0) ? 0 : 1;
				}

				if(bottom.Equals(ae.Bottom))
					ae.bottomLeftFill = ae.bottomRightFill = prevBottomFill;

				if(ClipOp.UNION == op)
				{
					topLeftFill  = (0 != ae.wc2) ? 1 : topLeftFill;
					topRightFill = (0 != ae.wc2) ? 1 : topRightFill;
				}
				else if(ClipOp.INTERSECTION == op)
				{
					topLeftFill  = (0 == ae.wc2) ? 0 : topLeftFill;
					topRightFill = (0 == ae.wc2) ? 0 : topRightFill;
				}

				ne = ae.nextActiveEdge;
				while(null != ne && !bottom.Equals(ae.Top) && Math.Abs(ae.bottomX - ne.bottomX).CompareTo(float.Epsilon) <= 0)
				{
					if(!bottom.Equals(ne.Top) && Math.Abs(ae.dx - ne.dx).CompareTo(float.Epsilon) <= 0)
					{
						ae.bundle = BundleState.CHILD;
						ne.bundle = BundleState.PARENT;
						break;
					}

					if(bottom.Equals(ne.Top))
						ne = ne.nextActiveEdge;
					else
						break;
				}

				if(BundleState.PARENT == ae.bundle)
					topLeftFill = prevTopFill;
				else if(BundleState.CHILD == ae.bundle)
					topLeftFill = topRightFill = prevTopFill;

				type = (topLeftFill<<12)|(topRightFill<<8)|(ae.bottomRightFill<<4)|(ae.bottomLeftFill);

				switch(type)
				{
					case 0x0001:
						if(!prevX.Equals(ae.bottomX) && null != ae.outList)
							ae.outList.PushFront(ae.bottomX, bottom, ae.Join);
						if(null != ae.outList && null != prevOut)
							AppendPolygon(ae.outList, prevOut, false);
						prevOut = null;
						break;
					case 0x0010:
						prevX   = ae.bottomX;
						prevOut = ae.outList;
						if(null != prevOut)
							prevOut.PushBack(ae.bottomX, bottom, ae.Join);
						break;
					case 0x0100:
						ae.outList = new Out(ae.bottomX, bottom, ae.Join);
						prevOut    = ae.outList;
						prevX      = ae.bottomX;
						break;
					case 0x0101:
						if(null != prevOut)
							prevOut.PushBack(ae.bottomX, bottom, true);
						if(null != ae.outList)
							ae.outList.PushFront(ae.bottomX, bottom, true);
						tempOut    = ae.outList;
						ae.outList = prevOut;
						prevOut    = tempOut;
						prevX      = ae.bottomX;
						break;
					case 0x0110:
						if(bottom.Equals(ae.EdgeBottom) && null != ae.outList)
							ae.outList.PushBack(ae.bottomX, bottom, ae.Join);
						break;
					case 0x0111:
						if(!prevX.Equals(ae.bottomX) && null != prevOut)
							prevOut.PushBack(ae.bottomX, bottom, ae.Join);
						ae.outList = prevOut;
						prevOut    = null;
						break;
					case 0x1000:
						if(!prevX.Equals(ae.bottomX) && null != prevOut)
							prevOut.PushFront(ae.bottomX, bottom, ae.Join);
						ae.outList = prevOut;
						prevOut    = null;
						break;
					case 0x1001:
						if(bottom.Equals(ae.EdgeBottom) && null != ae.outList)
							ae.outList.PushFront(ae.bottomX, bottom, ae.Join);
						break;
					case 0x1010:
						if(null != prevOut)
							prevOut.PushFront(ae.bottomX, bottom, true);
						if(null != ae.outList)
							ae.outList.PushBack(ae.bottomX, bottom, true);
						tempOut    = ae.outList;
						ae.outList = prevOut;
						prevOut    = tempOut;
						prevX      = ae.bottomX;
						break;
					case 0x1011:
						ae.outList = new Out(ae.bottomX, bottom, ae.Join);
						prevOut    = ae.outList;
						prevX      = ae.bottomX;
						break;
					case 0x1101:
						prevX   = ae.bottomX;
						prevOut = ae.outList;
						if(null != prevOut)
							prevOut.PushFront(ae.bottomX, bottom, ae.Join);
						break;
					case 0x1110:
						if(!prevX.Equals(ae.bottomX) && null != ae.outList)
							ae.outList.PushBack(ae.bottomX, bottom, ae.Join);
						if(null != ae.outList && null != prevOut)
							AppendPolygon(prevOut, ae.outList, true);
						prevOut = null;
						break;
				}

				prevTopFill        = topRightFill;
				prevBottomFill     = ae.bottomRightFill;
				ae.bottomLeftFill  = topLeftFill;
				ae.bottomRightFill = topRightFill;

				if(bottom.Equals(ae.Top))
				{
					ae = RemoveActiveEdge(ae);
				}
				else
				{
					ae.SetTopX(top);
					ae = ae.nextActiveEdge;
				}
			}
		}

		static void UpdateActiveEdges(float top)
		{
			ClipperBound ae = activeEdgeList;

			while(null != ae)
			{
				if(ae.Update(top))
					AddScanBeam(ae.EdgeTop);

				ae = ae.nextActiveEdge;
			}
		}

		public static void Clip(ClipOp op, Path subject, Path clip, Path result)
		{
			ClipperBound lm;
			float bottom, top = 0.0f;

			Clear();
			result.Clear();

			outPath = result;

			CreateLocalMinimaList(subject, 0);
			CreateLocalMinimaList(clip, 1);

			lm = localMinimaList;

			while(0 != numScanBeams)
			{
				bottom = scanBeam[--numScanBeams];

				while(null != lm && bottom.Equals(lm.Bottom))
				{
					AddActiveEdge(lm);
					AddScanBeam(lm.EdgeTop);

					lm = lm.nextLocalMinima;
				}

				if(0 != numScanBeams)
					top = scanBeam[numScanBeams-1];

				ProcessActiveEdges(op, top, bottom);
				CreateIntersections(top, bottom);
				ProcessIntersections(op);
				UpdateActiveEdges(top);
			}
		}
	}
}