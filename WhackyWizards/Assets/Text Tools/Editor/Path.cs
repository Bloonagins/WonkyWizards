// Copyright (C) 2019 Alexander Klochkov - All Rights Reserved
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms


using UnityEngine;

using System.Collections.Generic;


namespace texttools
{
	public enum FillRule 
    {
        EVEN_ODD = 0,
        NON_ZERO = 1
    }


    public struct Point 
    {
        public float x;  
        public float y;   
		public bool  join;    
    }	

	
	public struct SubPath
    {
        public Point[] points;
    }  
	
	
	public class Path 
    {
        public List<Point>   pointList;
        public List<SubPath> subPathList; 	
		
		public FillRule      fillRule;
        public Point         lastPos;
		
        static public float  approximationScale = 4.0f;	
        static public int    maxRecursionLevel  = 16;  
		
		
		public float Length
		{ 
			get 
			{
				return (pointList.Count > 1) ? Mathf.Sqrt((pointList[pointList.Count-1].x - pointList[pointList.Count-2].x)*(pointList[pointList.Count-1].x - pointList[pointList.Count-2].x) + (pointList[pointList.Count-1].y - pointList[pointList.Count-2].y)*(pointList[pointList.Count-1].y - pointList[pointList.Count-2].y)) : 0.0f;
			} 
		}
		
		public Path() 
        {
            pointList    = new List<Point>(); 
            subPathList  = new List<SubPath>();
			fillRule     = FillRule.NON_ZERO;  
			lastPos	     = default(Point);	
			lastPos.join = true; 
        }

        public void Clear() 
        {
            pointList.Clear();
            subPathList.Clear();

			fillRule     = FillRule.NON_ZERO; 
			lastPos	     = default(Point);	
			lastPos.join = true;
        } 
		
		float SquareDistance(float x1, float y1, float x2, float y2) 
        {
            float dx = x2 - x1;
            float dy = y2 - y1;

            return dx*dx + dy*dy;
        }  
		
		void RecursiveQuadraticCurve(float x1, float y1, float x2, float y2, float x3, float y3, int level) 
        {
            if(level > maxRecursionLevel) 
                return;

            float x12  = (x1 + x2) * 0.5f;                
            float y12  = (y1 + y2) * 0.5f;
            float x23  = (x2 + x3) * 0.5f;
            float y23  = (y2 + y3) * 0.5f;
            float x123 = (x12 + x23) * 0.5f;
            float y123 = (y12 + y23) * 0.5f;
            
            float dx = x3 - x1;
            float dy = y3 - y1;
            float d  = Mathf.Abs(((x2 - x3) * dy - (y2 - y3) * dx));            

			if((d * d).CompareTo(approximationScale * (dx*dx + dy*dy)) <= 0) 		  
			{
				AddPoint(x123, y123, false);
				return;
			}
			
            RecursiveQuadraticCurve(x1, y1, x12, y12, x123, y123, level + 1); 
            RecursiveQuadraticCurve(x123, y123, x23, y23, x3, y3, level + 1);
        } 
		
		private void RecursiveCubicCurve(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, int level) 
        {
            if(level > maxRecursionLevel) 
                return;
            
            float x12   = 0.5f * (x1 + x2);
            float y12   = 0.5f * (y1 + y2);
            float x23   = 0.5f * (x2 + x3);
            float y23   = 0.5f * (y2 + y3);
            float x34   = 0.5f * (x3 + x4);
            float y34   = 0.5f * (y3 + y4);
            float x123  = 0.5f * (x12 + x23);
            float y123  = 0.5f * (y12 + y23);
            float x234  = 0.5f * (x23 + x34);
            float y234  = 0.5f * (y23 + y34);
            float x1234 = 0.5f * (x123 + x234);
            float y1234 = 0.5f * (y123 + y234);

			float dx = x4 - x1;
            float dy = y4 - y1;
            float d2 = Mathf.Abs((x2 - x4) * dy - (y2 - y4) * dx);
            float d3 = Mathf.Abs((x3 - x4) * dy - (y3 - y4) * dx);
			
			if(((d2 + d3)*(d2 + d3)).CompareTo(approximationScale * (dx*dx + dy*dy)) <= 0)
            {
                AddPoint(x1234, y1234, false);
                return;
            } 
			
            RecursiveCubicCurve(x1, y1, x12, y12, x123, y123, x1234, y1234, level + 1); 
            RecursiveCubicCurve(x1234, y1234, x234, y234, x34, y34, x4, y4, level + 1);
        }	
		
		public void AddPoint(float x, float y, bool join) 
        {
            Point p;
            
            p.x    = x; 
            p.y    = y; 
			p.join = join;
			
            pointList.Add(p);
        } 
		
		public void MoveTo(float x, float y) 
        {
            lastPos.x = x;
			lastPos.y = y;
			
			Finalize(true);

			if(1 == pointList.Count)
				pointList[0] = lastPos;
			else
				AddPoint(x, y, true);			
        } 
		
		public void LineTo(float x, float y)
		{
			lastPos.x = x;
			lastPos.y = y;
	
			AddPoint(x, y, true);
		}  
		
		public void QuadraticCurveTo(float x, float y, float x1, float y1)
		{
			RecursiveQuadraticCurve(lastPos.x, lastPos.y, x1, y1, x, y, 0);
			AddPoint(x, y, true); 
	
			lastPos.x = x;	
			lastPos.y = y; 
		}	

		public void CubicCurveTo(float x, float y, float x1, float y1, float x2, float y2)
		{
			RecursiveCubicCurve(lastPos.x, lastPos.y, x1, y1, x2, y2, x, y, 0);
			AddPoint(x, y, true); 
	
			lastPos.x = x;
			lastPos.y = y;	
		} 


		public void Finalize(bool close)
		{
			if(pointList.Count > 2)
			{
				SubPath sp;

				sp.points = pointList.ToArray();

				subPathList.Add(sp);
				pointList.Clear();
			}
		}  
	}
}