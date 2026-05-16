// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PathFinder.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The path finder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Designer
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    // Note: I couldn't find a useful open source library that does
    // orthogonal routing so started to write something on my own.
    // Categorize this as a quick and dirty short term solution.
    // I will keep on searching.

    // Helper class to provide an orthogonal connection path
    /// <summary>
    /// The path finder.
    /// </summary>
    internal class PathFinder
    {
        #region Constants

        /// <summary>
        /// The margin.
        /// </summary>
        private const int margin = 20;

        #endregion

        #region Methods

        /// <summary>
        /// The get connection line.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="sink">
        /// The sink.
        /// </param>
        /// <param name="showLastLine">
        /// The show last line.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        internal static List<Point> GetConnectionLine(ConnectorInfo source, ConnectorInfo sink, bool showLastLine)
        {
            var linePoints = new List<Point>();

            Rect rectSource = GetRectWithMargin(source, margin);
            Rect rectSink = GetRectWithMargin(sink, margin);

            Point startPoint = GetOffsetPoint(source, rectSource);
            Point endPoint = GetOffsetPoint(sink, rectSink);

            linePoints.Add(startPoint);
            Point currentPoint = startPoint;

            if (!rectSink.Contains(currentPoint) && !rectSource.Contains(endPoint))
            {
                while (true)
                {
                    

                    if (IsPointVisible(currentPoint, endPoint, new[] { rectSource, rectSink }))
                    {
                        linePoints.Add(endPoint);
                        currentPoint = endPoint;
                        break;
                    }

                    Point neighbour = GetNearestVisibleNeighborSink(currentPoint, endPoint, sink, rectSource, rectSink);
                    if (!double.IsNaN(neighbour.X))
                    {
                        linePoints.Add(neighbour);
                        linePoints.Add(endPoint);
                        currentPoint = endPoint;
                        break;
                    }

                    if (currentPoint == startPoint)
                    {
                        bool flag;
                        Point n = GetNearestNeighborSource(source, endPoint, rectSource, rectSink, out flag);
                        linePoints.Add(n);
                        currentPoint = n;

                        if (!IsRectVisible(currentPoint, rectSink, new[] { rectSource }))
                        {
                            Point n1, n2;
                            GetOppositeCorners(source.Orientation, rectSource, out n1, out n2);
                            if (flag)
                            {
                                linePoints.Add(n1);
                                currentPoint = n1;
                            }
                            else
                            {
                                linePoints.Add(n2);
                                currentPoint = n2;
                            }

                            if (!IsRectVisible(currentPoint, rectSink, new[] { rectSource }))
                            {
                                if (flag)
                                {
                                    linePoints.Add(n2);
                                    currentPoint = n2;
                                }
                                else
                                {
                                    linePoints.Add(n1);
                                    currentPoint = n1;
                                }
                            }
                        }
                    }
                        

                        #region sink node
                    else
                    {
                        // from here on we jump to the sink node
                        Point n1, n2; // neighbour corner
                        Point s1, s2; // opposite corner
                        GetNeighborCorners(sink.Orientation, rectSink, out s1, out s2);
                        GetOppositeCorners(sink.Orientation, rectSink, out n1, out n2);

                        bool n1Visible = IsPointVisible(currentPoint, n1, new[] { rectSource, rectSink });
                        bool n2Visible = IsPointVisible(currentPoint, n2, new[] { rectSource, rectSink });

                        if (n1Visible && n2Visible)
                        {
                            if (rectSource.Contains(n1))
                            {
                                linePoints.Add(n2);
                                if (rectSource.Contains(s2))
                                {
                                    linePoints.Add(n1);
                                    linePoints.Add(s1);
                                }
                                else
                                {
                                    linePoints.Add(s2);
                                }

                                linePoints.Add(endPoint);
                                currentPoint = endPoint;
                                break;
                            }

                            if (rectSource.Contains(n2))
                            {
                                linePoints.Add(n1);
                                if (rectSource.Contains(s1))
                                {
                                    linePoints.Add(n2);
                                    linePoints.Add(s2);
                                }
                                else
                                {
                                    linePoints.Add(s1);
                                }

                                linePoints.Add(endPoint);
                                currentPoint = endPoint;
                                break;
                            }

                            if (Distance(n1, endPoint) <= Distance(n2, endPoint))
                            {
                                linePoints.Add(n1);
                                if (rectSource.Contains(s1))
                                {
                                    linePoints.Add(n2);
                                    linePoints.Add(s2);
                                }
                                else
                                {
                                    linePoints.Add(s1);
                                }

                                linePoints.Add(endPoint);
                                currentPoint = endPoint;
                                break;
                            }

                            linePoints.Add(n2);
                            if (rectSource.Contains(s2))
                            {
                                linePoints.Add(n1);
                                linePoints.Add(s1);
                            }
                            else
                            {
                                linePoints.Add(s2);
                            }

                            linePoints.Add(endPoint);
                            currentPoint = endPoint;
                            break;
                        }

                        if (n1Visible)
                        {
                            linePoints.Add(n1);
                            if (rectSource.Contains(s1))
                            {
                                linePoints.Add(n2);
                                linePoints.Add(s2);
                            }
                            else
                            {
                                linePoints.Add(s1);
                            }

                            linePoints.Add(endPoint);
                            currentPoint = endPoint;
                            break;
                        }

                        linePoints.Add(n2);
                        if (rectSource.Contains(s2))
                        {
                            linePoints.Add(n1);
                            linePoints.Add(s1);
                        }
                        else
                        {
                            linePoints.Add(s2);
                        }

                        linePoints.Add(endPoint);
                        currentPoint = endPoint;
                        break;
                    }

                    #endregion
                }
            }
            else
            {
                linePoints.Add(endPoint);
            }

            linePoints = OptimizeLinePoints(
                linePoints, 
                new[] { rectSource, rectSink }, 
                source.Orientation, 
                sink.Orientation);

            CheckPathEnd(source, sink, showLastLine, linePoints);
            return linePoints;
        }

        /// <summary>
        /// The get connection line.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="sinkPoint">
        /// The sink point.
        /// </param>
        /// <param name="preferredOrientation">
        /// The preferred orientation.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        internal static List<Point> GetConnectionLine(
            ConnectorInfo source, 
            Point sinkPoint, 
            ConnectorOrientation preferredOrientation)
        {
            var linePoints = new List<Point>();
            Rect rectSource = GetRectWithMargin(source, 10);
            Point startPoint = GetOffsetPoint(source, rectSource);
            Point endPoint = sinkPoint;

            linePoints.Add(startPoint);
            Point currentPoint = startPoint;

            if (!rectSource.Contains(endPoint))
            {
                while (true)
                {
                    if (IsPointVisible(currentPoint, endPoint, new[] { rectSource }))
                    {
                        linePoints.Add(endPoint);
                        break;
                    }

                    bool sideFlag;
                    Point n = GetNearestNeighborSource(source, endPoint, rectSource, out sideFlag);
                    linePoints.Add(n);
                    currentPoint = n;

                    if (IsPointVisible(currentPoint, endPoint, new[] { rectSource }))
                    {
                        linePoints.Add(endPoint);
                        break;
                    }

                    Point n1, n2;
                    GetOppositeCorners(source.Orientation, rectSource, out n1, out n2);
                    if (sideFlag)
                    {
                        linePoints.Add(n1);
                    }
                    else
                    {
                        linePoints.Add(n2);
                    }

                    linePoints.Add(endPoint);
                    break;
                }
            }
            else
            {
                linePoints.Add(endPoint);
            }

            if (preferredOrientation != ConnectorOrientation.None)
            {
                linePoints = OptimizeLinePoints(
                    linePoints, 
                    new[] { rectSource }, 
                    source.Orientation, 
                    preferredOrientation);
            }
            else
            {
                linePoints = OptimizeLinePoints(
                    linePoints, 
                    new[] { rectSource }, 
                    source.Orientation, 
                    GetOpositeOrientation(source.Orientation));
            }

            return linePoints;
        }

        /// <summary>
        /// The check path end.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="sink">
        /// The sink.
        /// </param>
        /// <param name="showLastLine">
        /// The show last line.
        /// </param>
        /// <param name="linePoints">
        /// The line points.
        /// </param>
        private static void CheckPathEnd(
            ConnectorInfo source, 
            ConnectorInfo sink, 
            bool showLastLine, 
            List<Point> linePoints)
        {
            if (showLastLine)
            {
                var startPoint = new Point(0, 0);
                var endPoint = new Point(0, 0);
                double marginPath = 15;
                switch (source.Orientation)
                {
                    case ConnectorOrientation.Left:
                        startPoint = new Point(source.Position.X - marginPath, source.Position.Y);
                        break;
                    case ConnectorOrientation.Top:
                        startPoint = new Point(source.Position.X, source.Position.Y - marginPath);
                        break;
                    case ConnectorOrientation.Right:
                        startPoint = new Point(source.Position.X + marginPath, source.Position.Y);
                        break;
                    case ConnectorOrientation.Bottom:
                        startPoint = new Point(source.Position.X, source.Position.Y + marginPath);
                        break;
                    default:
                        break;
                }

                switch (sink.Orientation)
                {
                    case ConnectorOrientation.Left:
                        endPoint = new Point(sink.Position.X - marginPath, sink.Position.Y);
                        break;
                    case ConnectorOrientation.Top:
                        endPoint = new Point(sink.Position.X, sink.Position.Y - marginPath);
                        break;
                    case ConnectorOrientation.Right:
                        endPoint = new Point(sink.Position.X + marginPath, sink.Position.Y);
                        break;
                    case ConnectorOrientation.Bottom:
                        endPoint = new Point(sink.Position.X, sink.Position.Y + marginPath);
                        break;
                    default:
                        break;
                }

                linePoints.Insert(0, startPoint);
                linePoints.Add(endPoint);
            }
            else
            {
                linePoints.Insert(0, source.Position);
                linePoints.Add(sink.Position);
            }
        }

        /// <summary>
        /// The distance.
        /// </summary>
        /// <param name="p1">
        /// The p 1.
        /// </param>
        /// <param name="p2">
        /// The p 2.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        private static double Distance(Point p1, Point p2)
        {
            return Point.Subtract(p1, p2).Length;
        }

        /// <summary>
        /// The get nearest neighbor source.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="endPoint">
        /// The end point.
        /// </param>
        /// <param name="rectSource">
        /// The rect source.
        /// </param>
        /// <param name="rectSink">
        /// The rect sink.
        /// </param>
        /// <param name="flag">
        /// The flag.
        /// </param>
        /// <returns>
        /// The <see cref="Point"/>.
        /// </returns>
        private static Point GetNearestNeighborSource(
            ConnectorInfo source, 
            Point endPoint, 
            Rect rectSource, 
            Rect rectSink, 
            out bool flag)
        {
            Point n1, n2; // neighbors
            GetNeighborCorners(source.Orientation, rectSource, out n1, out n2);

            if (rectSink.Contains(n1))
            {
                flag = false;
                return n2;
            }

            if (rectSink.Contains(n2))
            {
                flag = true;
                return n1;
            }

            if (Distance(n1, endPoint) <= Distance(n2, endPoint))
            {
                flag = true;
                return n1;
            }

            flag = false;
            return n2;
        }

        /// <summary>
        /// The get nearest neighbor source.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="endPoint">
        /// The end point.
        /// </param>
        /// <param name="rectSource">
        /// The rect source.
        /// </param>
        /// <param name="flag">
        /// The flag.
        /// </param>
        /// <returns>
        /// The <see cref="Point"/>.
        /// </returns>
        private static Point GetNearestNeighborSource(
            ConnectorInfo source, 
            Point endPoint, 
            Rect rectSource, 
            out bool flag)
        {
            Point n1, n2; // neighbors
            GetNeighborCorners(source.Orientation, rectSource, out n1, out n2);

            if (Distance(n1, endPoint) <= Distance(n2, endPoint))
            {
                flag = true;
                return n1;
            }

            flag = false;
            return n2;
        }

        /// <summary>
        /// The get nearest visible neighbor sink.
        /// </summary>
        /// <param name="currentPoint">
        /// The current point.
        /// </param>
        /// <param name="endPoint">
        /// The end point.
        /// </param>
        /// <param name="sink">
        /// The sink.
        /// </param>
        /// <param name="rectSource">
        /// The rect source.
        /// </param>
        /// <param name="rectSink">
        /// The rect sink.
        /// </param>
        /// <returns>
        /// The <see cref="Point"/>.
        /// </returns>
        private static Point GetNearestVisibleNeighborSink(
            Point currentPoint, 
            Point endPoint, 
            ConnectorInfo sink, 
            Rect rectSource, 
            Rect rectSink)
        {
            Point s1, s2; // neighbors on sink side
            GetNeighborCorners(sink.Orientation, rectSink, out s1, out s2);

            bool flag1 = IsPointVisible(currentPoint, s1, new[] { rectSource, rectSink });
            bool flag2 = IsPointVisible(currentPoint, s2, new[] { rectSource, rectSink });

            if (flag1)
            {
                // s1 visible
                if (flag2)
                {
                    // s1 and s2 visible
                    if (rectSink.Contains(s1))
                    {
                        return s2;
                    }

                    if (rectSink.Contains(s2))
                    {
                        return s1;
                    }

                    if (Distance(s1, endPoint) <= Distance(s2, endPoint))
                    {
                        return s1;
                    }

                    return s2;
                }

                return s1;
            }

            if (flag2)
            {
                // only s2 visible
                return s2;
            }

            return new Point(double.NaN, double.NaN);
        }

        /// <summary>
        /// The get neighbor corners.
        /// </summary>
        /// <param name="orientation">
        /// The orientation.
        /// </param>
        /// <param name="rect">
        /// The rect.
        /// </param>
        /// <param name="n1">
        /// The n 1.
        /// </param>
        /// <param name="n2">
        /// The n 2.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        private static void GetNeighborCorners(ConnectorOrientation orientation, Rect rect, out Point n1, out Point n2)
        {
            switch (orientation)
            {
                case ConnectorOrientation.Left:
                    n1 = rect.TopLeft;
                    n2 = rect.BottomLeft;
                    break;
                case ConnectorOrientation.Top:
                    n1 = rect.TopLeft;
                    n2 = rect.TopRight;
                    break;
                case ConnectorOrientation.Right:
                    n1 = rect.TopRight;
                    n2 = rect.BottomRight;
                    break;
                case ConnectorOrientation.Bottom:
                    n1 = rect.BottomLeft;
                    n2 = rect.BottomRight;
                    break;
                default:
                    throw new Exception("No neighour corners found!");
            }
        }

        /// <summary>
        /// The get offset point.
        /// </summary>
        /// <param name="connector">
        /// The connector.
        /// </param>
        /// <param name="rect">
        /// The rect.
        /// </param>
        /// <returns>
        /// The <see cref="Point"/>.
        /// </returns>
        private static Point GetOffsetPoint(ConnectorInfo connector, Rect rect)
        {
            var offsetPoint = new Point();

            switch (connector.Orientation)
            {
                case ConnectorOrientation.Left:
                    offsetPoint = new Point(rect.Left, connector.Position.Y);
                    break;
                case ConnectorOrientation.Top:
                    offsetPoint = new Point(connector.Position.X, rect.Top);
                    break;
                case ConnectorOrientation.Right:
                    offsetPoint = new Point(rect.Right, connector.Position.Y);
                    break;
                case ConnectorOrientation.Bottom:
                    offsetPoint = new Point(connector.Position.X, rect.Bottom);
                    break;
                default:
                    break;
            }

            return offsetPoint;
        }

        /// <summary>
        /// The get oposite orientation.
        /// </summary>
        /// <param name="connectorOrientation">
        /// The connector orientation.
        /// </param>
        /// <returns>
        /// The <see cref="ConnectorOrientation"/>.
        /// </returns>
        private static ConnectorOrientation GetOpositeOrientation(ConnectorOrientation connectorOrientation)
        {
            switch (connectorOrientation)
            {
                case ConnectorOrientation.Left:
                    return ConnectorOrientation.Right;
                case ConnectorOrientation.Top:
                    return ConnectorOrientation.Bottom;
                case ConnectorOrientation.Right:
                    return ConnectorOrientation.Left;
                case ConnectorOrientation.Bottom:
                    return ConnectorOrientation.Top;
                default:
                    return ConnectorOrientation.Top;
            }
        }

        /// <summary>
        /// The get opposite corners.
        /// </summary>
        /// <param name="orientation">
        /// The orientation.
        /// </param>
        /// <param name="rect">
        /// The rect.
        /// </param>
        /// <param name="n1">
        /// The n 1.
        /// </param>
        /// <param name="n2">
        /// The n 2.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        private static void GetOppositeCorners(ConnectorOrientation orientation, Rect rect, out Point n1, out Point n2)
        {
            switch (orientation)
            {
                case ConnectorOrientation.Left:
                    n1 = rect.TopRight;
                    n2 = rect.BottomRight;
                    break;
                case ConnectorOrientation.Top:
                    n1 = rect.BottomLeft;
                    n2 = rect.BottomRight;
                    break;
                case ConnectorOrientation.Right:
                    n1 = rect.TopLeft;
                    n2 = rect.BottomLeft;
                    break;
                case ConnectorOrientation.Bottom:
                    n1 = rect.TopLeft;
                    n2 = rect.TopRight;
                    break;
                default:
                    throw new Exception("No opposite corners found!");
            }
        }

        /// <summary>
        /// The get orientation.
        /// </summary>
        /// <param name="p1">
        /// The p 1.
        /// </param>
        /// <param name="p2">
        /// The p 2.
        /// </param>
        /// <returns>
        /// The <see cref="ConnectorOrientation"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        private static ConnectorOrientation GetOrientation(Point p1, Point p2)
        {
            if (p1.X == p2.X)
            {
                if (p1.Y >= p2.Y)
                {
                    return ConnectorOrientation.Bottom;
                }

                return ConnectorOrientation.Top;
            }

            if (p1.Y == p2.Y)
            {
                if (p1.X >= p2.X)
                {
                    return ConnectorOrientation.Right;
                }

                return ConnectorOrientation.Left;
            }

            throw new Exception("Failed to retrieve orientation");
        }

        /// <summary>
        /// The get orientation.
        /// </summary>
        /// <param name="sourceOrientation">
        /// The source orientation.
        /// </param>
        /// <returns>
        /// The <see cref="Orientation"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        private static Orientation GetOrientation(ConnectorOrientation sourceOrientation)
        {
            switch (sourceOrientation)
            {
                case ConnectorOrientation.Left:
                    return Orientation.Horizontal;
                case ConnectorOrientation.Top:
                    return Orientation.Vertical;
                case ConnectorOrientation.Right:
                    return Orientation.Horizontal;
                case ConnectorOrientation.Bottom:
                    return Orientation.Vertical;
                default:
                    throw new Exception("Unknown ConnectorOrientation");
            }
        }

        /// <summary>
        /// The get rect with margin.
        /// </summary>
        /// <param name="connectorThumb">
        /// The connector thumb.
        /// </param>
        /// <param name="margin">
        /// The margin.
        /// </param>
        /// <returns>
        /// The <see cref="Rect"/>.
        /// </returns>
        private static Rect GetRectWithMargin(ConnectorInfo connectorThumb, double margin)
        {
            var rect = new Rect(
                connectorThumb.DesignerItemLeft, 
                connectorThumb.DesignerItemTop, 
                connectorThumb.DesignerItemSize.Width, 
                connectorThumb.DesignerItemSize.Height);

            rect.Inflate(margin, margin);

            return rect;
        }

        /// <summary>
        /// The is point visible.
        /// </summary>
        /// <param name="fromPoint">
        /// The from point.
        /// </param>
        /// <param name="targetPoint">
        /// The target point.
        /// </param>
        /// <param name="rectangles">
        /// The rectangles.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool IsPointVisible(Point fromPoint, Point targetPoint, Rect[] rectangles)
        {
            foreach (var rect in rectangles)
            {
                if (RectangleIntersectsLine(rect, fromPoint, targetPoint))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// The is rect visible.
        /// </summary>
        /// <param name="fromPoint">
        /// The from point.
        /// </param>
        /// <param name="targetRect">
        /// The target rect.
        /// </param>
        /// <param name="rectangles">
        /// The rectangles.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool IsRectVisible(Point fromPoint, Rect targetRect, Rect[] rectangles)
        {
            if (IsPointVisible(fromPoint, targetRect.TopLeft, rectangles))
            {
                return true;
            }

            if (IsPointVisible(fromPoint, targetRect.TopRight, rectangles))
            {
                return true;
            }

            if (IsPointVisible(fromPoint, targetRect.BottomLeft, rectangles))
            {
                return true;
            }

            if (IsPointVisible(fromPoint, targetRect.BottomRight, rectangles))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// The optimize line points.
        /// </summary>
        /// <param name="linePoints">
        /// The line points.
        /// </param>
        /// <param name="rectangles">
        /// The rectangles.
        /// </param>
        /// <param name="sourceOrientation">
        /// The source orientation.
        /// </param>
        /// <param name="sinkOrientation">
        /// The sink orientation.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        private static List<Point> OptimizeLinePoints(
            List<Point> linePoints, 
            Rect[] rectangles, 
            ConnectorOrientation sourceOrientation, 
            ConnectorOrientation sinkOrientation)
        {
            var points = new List<Point>();
            int cut = 0;

            for (int i = 0; i < linePoints.Count; i++)
            {
                if (i >= cut)
                {
                    for (int k = linePoints.Count - 1; k > i; k--)
                    {
                        if (IsPointVisible(linePoints[i], linePoints[k], rectangles))
                        {
                            cut = k;
                            break;
                        }
                    }

                    points.Add(linePoints[i]);
                }
            }

            

            for (int j = 0; j < points.Count - 1; j++)
            {
                if (points[j].X != points[j + 1].X && points[j].Y != points[j + 1].Y)
                {
                    ConnectorOrientation orientationFrom;
                    ConnectorOrientation orientationTo;

                    // orientation from point
                    if (j == 0)
                    {
                        orientationFrom = sourceOrientation;
                    }
                    else
                    {
                        orientationFrom = GetOrientation(points[j], points[j - 1]);
                    }

                    // orientation to pint 
                    if (j == points.Count - 2)
                    {
                        orientationTo = sinkOrientation;
                    }
                    else
                    {
                        orientationTo = GetOrientation(points[j + 1], points[j + 2]);
                    }

                    if ((orientationFrom == ConnectorOrientation.Left || orientationFrom == ConnectorOrientation.Right)
                        && (orientationTo == ConnectorOrientation.Left || orientationTo == ConnectorOrientation.Right))
                    {
                        double centerX = Math.Min(points[j].X, points[j + 1].X)
                                         + Math.Abs(points[j].X - points[j + 1].X) / 2;
                        points.Insert(j + 1, new Point(centerX, points[j].Y));
                        points.Insert(j + 2, new Point(centerX, points[j + 2].Y));
                        if (points.Count - 1 > j + 3)
                        {
                            points.RemoveAt(j + 3);
                        }

                        return points;
                    }

                    if ((orientationFrom == ConnectorOrientation.Top || orientationFrom == ConnectorOrientation.Bottom)
                        && (orientationTo == ConnectorOrientation.Top || orientationTo == ConnectorOrientation.Bottom))
                    {
                        double centerY = Math.Min(points[j].Y, points[j + 1].Y)
                                         + Math.Abs(points[j].Y - points[j + 1].Y) / 2;
                        points.Insert(j + 1, new Point(points[j].X, centerY));
                        points.Insert(j + 2, new Point(points[j + 2].X, centerY));
                        if (points.Count - 1 > j + 3)
                        {
                            points.RemoveAt(j + 3);
                        }

                        return points;
                    }

                    if ((orientationFrom == ConnectorOrientation.Left || orientationFrom == ConnectorOrientation.Right)
                        && (orientationTo == ConnectorOrientation.Top || orientationTo == ConnectorOrientation.Bottom))
                    {
                        points.Insert(j + 1, new Point(points[j + 1].X, points[j].Y));
                        return points;
                    }

                    if ((orientationFrom == ConnectorOrientation.Top || orientationFrom == ConnectorOrientation.Bottom)
                        && (orientationTo == ConnectorOrientation.Left || orientationTo == ConnectorOrientation.Right))
                    {
                        points.Insert(j + 1, new Point(points[j].X, points[j + 1].Y));
                        return points;
                    }
                }
            }

            

            return points;
        }

        /// <summary>
        /// The rectangle intersects line.
        /// </summary>
        /// <param name="rect">
        /// The rect.
        /// </param>
        /// <param name="startPoint">
        /// The start point.
        /// </param>
        /// <param name="endPoint">
        /// The end point.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool RectangleIntersectsLine(Rect rect, Point startPoint, Point endPoint)
        {
            rect.Inflate(-1, -1);
            return rect.IntersectsWith(new Rect(startPoint, endPoint));
        }

        #endregion
    }
}