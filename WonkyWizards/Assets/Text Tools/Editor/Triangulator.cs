// Copyright (C) 2019 Alexander Klochkov - All Rights Reserved
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms


using System.Collections.Generic;


namespace texttools
{
	internal struct MonotonePoint
	{
		internal int  id;
		internal bool left;
		internal bool merge;
	}

	internal class Bound
	{
		internal Bound               nextLocalMinima;
		internal Bound               nextActiveEdge;
		internal Bound               prevActiveEdge;
		internal Bound               helper;

		internal int                 begin;
		internal int                 end;
		internal int                 upper;
		internal int                 lower;
		internal int                 direction;
		internal int                 offset;

		internal float               dx;
		internal float               bottomX;

		internal List<MonotonePoint> monotone;
		internal MonotonePoint       mp;
		internal MonotonePoint       helperPoint;

		internal Point[]             pointList;

		internal float Top
		{
			get { return pointList[end].y; }
		}

		internal float Bottom
		{
			get { return pointList[begin].y; }
		}

		internal float EdgeTop
		{
			get { return pointList[upper].y; }
		}

		internal float EdgeBottom
		{
			get { return pointList[lower].y; }
		}

		internal Bound(Point[] points, int begin, int end, int direction, int offset)
		{
			this.nextLocalMinima = null;
			this.nextActiveEdge  = null;
			this.prevActiveEdge  = null;
			this.helper          = null;

			this.begin           = begin;
			this.end             = end;
			this.upper           = (begin + direction + points.Length) % points.Length;
			this.lower           = begin;
			this.direction       = direction;
			this.offset          = offset;

			this.dx              = (points[upper].x - points[lower].x) / (points[upper].y - points[lower].y);
			this.bottomX         = points[begin].x;

			this.monotone        = null;
			this.mp              = default(MonotonePoint);
			this.helperPoint     = default(MonotonePoint);
			this.mp.id           = lower + offset;

			this.pointList       = points;
		}


		internal bool Update(float top)
		{
			if(EdgeTop == top && upper != end)
			{
				lower   = upper;
				upper   = (upper + direction + pointList.Length) % pointList.Length;
				dx      = (pointList[upper].x - pointList[lower].x) / (pointList[upper].y - pointList[lower].y);
				bottomX = pointList[lower].x;
				mp.id   = lower + offset;

				return true;
			}

			if(Top == top)
				lower = upper;

			bottomX = (EdgeTop == top) ? pointList[upper].x : pointList[lower].x + dx * (top - EdgeBottom);
			mp.id   = lower + offset;

			return false;
		}
	}

	public static class Triangulator
	{
		public static List<Point> pointList       = new List<Point>();
		public static List<int>   indexList       = new List<int>();

		public static float       epsilon         = 0.0001f;

		static MonotonePoint[]    stack           = new MonotonePoint[4096];

		static Bound              localMinimaList = null;
		static Bound              activeEdgeList  = null;

		static float[]            scanBeam        = new float[8192];
		static int                numScanBeams    = 0;


		static void Clear()
		{
			Bound lm;

			while(null != localMinimaList)
			{
				lm = localMinimaList.nextLocalMinima;
				localMinimaList.nextLocalMinima = null;
				localMinimaList.nextActiveEdge  = null;
				localMinimaList.prevActiveEdge  = null;
				localMinimaList.monotone.Clear();
				localMinimaList.monotone = null;
				localMinimaList = null;
				localMinimaList = lm;
			}

			pointList.Clear();
			indexList.Clear();

			numScanBeams    = 0;
			localMinimaList = null;
			activeEdgeList  = null;
		}

		static void AddMonotonePoint(ref List<MonotonePoint> monotone, MonotonePoint mp, bool left)
		{
			mp.left = left;
			monotone.Add(mp);
		}

		static void AddLocalMinima(Bound lm)
		{
			Bound prev = null;
			Bound curr = localMinimaList;

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

		static void AddActiveEdge(Bound ae)
		{
			Bound prev = null;
			Bound curr = activeEdgeList;

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

		static Bound RemoveActiveEdge(Bound ae)
		{
			Bound prev = ae.prevActiveEdge;
			Bound next = ae.nextActiveEdge;

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

		static void CreateLocalMinimaList(Path path)
		{
			int count, prev, next, max, n, offset = 0;

			if(null == path)
				return;

			foreach(SubPath sp in path.subPathList)
			{
				count = sp.points.Length;
				if(count < 3)
					continue;

				for(int i=0; i<count; ++i)
					pointList.Add(sp.points[i]);
			}

			foreach(SubPath sp in path.subPathList)
			{
				count = sp.points.Length;
				if(count < 3)
					continue;

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

						AddLocalMinima(new Bound(sp.points, i, max, +1, offset));
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

						AddLocalMinima(new Bound(sp.points, i, max, -1, offset));
						AddScanBeam(sp.points[i].y);
					}

					i += n;
				}

				offset += count;
			}
		}

		static void ProcessActiveEdges(float top, float bottom)
		{
			List<MonotonePoint> prevMonotone    = null;
			MonotonePoint       mp              = default(MonotonePoint);
			Bound               ae              = activeEdgeList;
			int                 type            = 0;
			int                 topLeftFill     = 0;
			int                 topRightFill    = 0;
			int                 bottomLeftFill  = 0;
			int                 bottomRightFill = 0;
			int                 prevTopFill     = 0;
			int                 prevBottomFill  = 0;
			float               prevX           = float.MinValue;

			while(null != ae)
			{
				topLeftFill     = prevTopFill;
				bottomLeftFill  = prevBottomFill;
				topRightFill    = prevTopFill ^ 1;
				bottomRightFill = prevBottomFill ^ 1;

				if(ae.Top == bottom)
					topLeftFill = topRightFill = prevTopFill;

				if(ae.Bottom == bottom)
					bottomLeftFill = bottomRightFill = prevBottomFill;

				type = (topLeftFill<<12)|(topRightFill<<8)|(bottomRightFill<<4)|(bottomLeftFill);

				switch(type)
				{
					case 0x0001:
						if(!prevX.Equals(ae.bottomX))
						{
							mp = ae.monotone[ae.monotone.Count-1];
							mp.left = true;
							ae.monotone[ae.monotone.Count-1] = mp;
							AddMonotonePoint(ref ae.monotone, ae.mp, false);
						}

						TriangulateMonotone(ae.monotone);
						prevMonotone = null;
						break;
					case 0x0010:
						if(ae.helperPoint.merge)
						{
							AddMonotonePoint(ref ae.monotone, ae.mp, false);
							TriangulateMonotone(ae.monotone);
							ae.monotone = ae.helper.monotone;
						}

						AddMonotonePoint(ref ae.monotone, ae.mp, false);
						prevX        = ae.bottomX;
						prevMonotone = ae.monotone;
						break;
					case 0x0100:
						prevX          = ae.bottomX;
						prevMonotone   = new List<MonotonePoint>();
						ae.monotone    = prevMonotone;
						ae.helper      = ae;
						ae.helperPoint = ae.mp;
						AddMonotonePoint(ref ae.monotone, ae.mp, true);
						break;
					case 0x0110:
						if(bottom.Equals(ae.EdgeBottom))
						{
							if(ae.helperPoint.merge)
							{
								AddMonotonePoint(ref ae.monotone, ae.mp, false);
								TriangulateMonotone(ae.monotone);
								ae.monotone = ae.helper.monotone;
							}

							ae.helper      = ae;
							ae.helperPoint = ae.mp;
							AddMonotonePoint(ref ae.monotone, ae.mp, true);
						}
						break;
					case 0x0111:
						ae.monotone    = prevMonotone;
						ae.helper      = ae;
						ae.helperPoint = ae.mp;
						prevMonotone   = null;
						mp             = ae.monotone[ae.monotone.Count-1];
						mp.left        = true;
						ae.monotone[ae.monotone.Count-1] = mp;

						if(!prevX.Equals(ae.bottomX))
							AddMonotonePoint(ref ae.monotone, ae.mp, true);

						break;
					case 0x1000:
						ae.monotone  = prevMonotone;
						prevMonotone = null;

						if(!prevX.Equals(ae.bottomX))
						{
							AddMonotonePoint(ref ae.monotone, ae.mp, false);
							ae.prevActiveEdge.helper      = ae;
							ae.prevActiveEdge.helperPoint = ae.mp;
						}

						break;
					case 0x1001:
						if(bottom.Equals(ae.EdgeBottom))
						{
							if(ae.prevActiveEdge.helperPoint.merge)
							{
								AddMonotonePoint(ref ae.monotone, ae.mp, false);
								TriangulateMonotone(ae.monotone);
								ae.monotone = ae.prevActiveEdge.monotone;
							}

							ae.prevActiveEdge.helper      = ae;
							ae.prevActiveEdge.helperPoint = ae.mp;
							AddMonotonePoint(ref ae.monotone, ae.mp, false);
						}
						break;
					case 0x1011:
						if(ae.prevActiveEdge.helperPoint.merge)
						{
							prevMonotone = ae.prevActiveEdge.helper.monotone;
							ae.monotone  = prevMonotone;
							AddMonotonePoint(ref ae.monotone, ae.mp, true);
							ae.monotone  = ae.prevActiveEdge.monotone;
							AddMonotonePoint(ref ae.monotone, ae.mp, false);
						}
						else
						{
							prevMonotone = ae.prevActiveEdge.monotone;
							ae.prevActiveEdge.helper.monotone = new List<MonotonePoint>();
							AddMonotonePoint(ref ae.prevActiveEdge.helper.monotone, ae.prevActiveEdge.helperPoint, true);

							if(!prevMonotone[prevMonotone.Count-1].left)
								prevMonotone = ae.prevActiveEdge.helper.monotone;

							ae.monotone = prevMonotone;
							AddMonotonePoint(ref ae.monotone, ae.mp, true);

							ae.monotone = ae.prevActiveEdge.monotone;
							AddMonotonePoint(ref ae.monotone, ae.mp, false);
						}

						ae.prevActiveEdge.helper      = ae;
						ae.prevActiveEdge.helperPoint = ae.mp;
						ae.helper                     = ae;
						ae.helperPoint                = ae.mp;
						prevX                         = ae.bottomX;
						break;
					case 0x1101:
						if(ae.prevActiveEdge.helperPoint.merge)
						{
							AddMonotonePoint(ref ae.monotone, ae.mp, false);
							TriangulateMonotone(ae.monotone);
							ae.monotone = ae.prevActiveEdge.monotone;
						}

						prevX        = ae.bottomX;
						prevMonotone = ae.monotone;
						AddMonotonePoint(ref ae.monotone, ae.mp, false);
						break;
					case 0x1110:
						if(ae.helperPoint.merge)
						{
							AddMonotonePoint(ref ae.monotone, ae.mp, false);
							TriangulateMonotone(ae.monotone);
							ae.monotone = ae.helper.monotone;
						}

						if(!prevX.Equals(ae.bottomX))
							AddMonotonePoint(ref ae.monotone, ae.mp, true);

						mp      = prevMonotone[prevMonotone.Count-1];
						mp.left = true;
						ae.monotone.Add(mp);
						mp       = ae.mp;
						mp.merge = true;
						ae.prevActiveEdge.helper      = ae;
						ae.prevActiveEdge.helperPoint = mp;
						prevMonotone = null;
						break;
				}

				prevTopFill    = topRightFill;
				prevBottomFill = bottomRightFill;

				if(bottom.Equals(ae.Top))
					ae = RemoveActiveEdge(ae);
				else
					ae = ae.nextActiveEdge;
			}
		}

		static void UpdateActiveEdges(float top)
		{
			Bound ae = activeEdgeList;

			while(null != ae)
			{
				if(ae.Update(top))
					AddScanBeam(ae.EdgeTop);

				ae = ae.nextActiveEdge;
			}
		}

		static void TriangulateMonotone(List<MonotonePoint> m)
		{
			Point         e, c, d;
			MonotonePoint a, b, s, t;
			float         n;
			int           i, count, top = 2;

			if(m.Count < 3)
				return;

			stack[1] = m[0];
			stack[2] = m[1];
			count    = m.Count - 1;

			for(i=2; i<count; ++i)
			{
				a = m[i];
				b = stack[top];

				if(a.left != b.left)
				{
					while(top > 1)
					{
						s = stack[top--];
						t = stack[top];

						if(a.left)
						{
							indexList.Add(a.id);
							indexList.Add(t.id);
							indexList.Add(s.id);
						}
						else
						{
							indexList.Add(a.id);
							indexList.Add(s.id);
							indexList.Add(t.id);
						}
					}

					stack[top] = m[i-1];
					stack[++top] = m[i];
				}
				else
				{
					while(top > 1)
					{
						s = stack[top];
						t = stack[top-1];

						e = pointList[a.id];
						c = pointList[s.id];
						d = pointList[t.id];

						n = (e.x - c.x) * (d.y - c.y) - (e.y - c.y) * (d.x - c.x);
						if((n > 0.0f && s.left) || (n < 0.0f && !s.left))
						{
							if(n < 0.0f)
							{
								indexList.Add(a.id);
								indexList.Add(t.id);
								indexList.Add(s.id);
							}
							else
							{
								indexList.Add(a.id);
								indexList.Add(s.id);
								indexList.Add(t.id);
							}
							--top;
						}
						else
							break;
					}

					stack[++top] = m[i];
				}
			}

			a = m[i];
			while(top != 1)
			{
				s = stack[top--];
				t = stack[top];

				if(a.left == s.left)
				{
					indexList.Add(a.id);
					indexList.Add(t.id);
					indexList.Add(s.id);
				}
				else
				{
					indexList.Add(a.id);
					indexList.Add(s.id);
					indexList.Add(t.id);
				}
			}
		}

		public static void Triangulate(Path path)
		{
			Bound lm;
			float bottom, top = 0.0f;

			Clear();
			CreateLocalMinimaList(path);

			lm = localMinimaList;

			while(0 != numScanBeams)
			{
				bottom = scanBeam[--numScanBeams];

				while(null != lm && lm.Bottom == bottom)
				{
					AddActiveEdge(lm);
					AddScanBeam(lm.EdgeTop);

					lm = lm.nextLocalMinima;
				}

				if(0 != numScanBeams)
					top = scanBeam[numScanBeams-1];

				ProcessActiveEdges(top, bottom);
				UpdateActiveEdges(top);
			}
		}
	}
}