using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawHexagon
{

    /// <summary>
    /// http://csharphelper.com/blog/2015/10/draw-a-hexagonal-grid-in-c/
    /// </summary>
    class Hexagon
    {
        // Return the points that define the indicated hexagon.
        private PointF[] HexToPoints(float height, float row, float col)
        {
            // Start with the leftmost corner of the upper left hexagon.
            float width = HexWidth(height);
            float y = height / 2;
            float x = 0;

            // Move down the required number of rows.
            y += row * height;

            // If the column is odd, move down half a hex more.
            if (col % 2 == 1) y += height / 2;

            // Move over for the column number.
            x += col * (width * 0.75f);


            // Generate the points.
            return new PointF[]
                {
                    new PointF(x, y),
                    new PointF(x + width * 0.25f, y - height / 2),
                    new PointF(x + width * 0.75f, y - height / 2),
                    new PointF(x + width, y),
                    new PointF(x + width * 0.75f, y + height / 2),
                    new PointF(x + width * 0.25f, y + height / 2),
                };
        }

        // Return the width of a hexagon.
        private float HexWidth(float height)
        {
            return (float)(4 * (height / 2 / Math.Sqrt(3)));
        }

        // Draw a hexagonal grid for the indicated area.
        // (You might be able to draw the hexagons without
        // drawing any duplicate edges, but this is a lot easier.)
        public List<PointF> DrawHexGrid(float xmin, float xmax, float ymin, float ymax, float height)
        {
            List<PointF> result = new List<PointF>();
            //float[,] result = new float[,] { };

            // Loop until a hexagon won't fit.
            for (int row = 0; ; row++)
            {
                //// Get the points for the row's first hexagon.
                PointF[] points = HexToPoints(height, row, 0);

                //// If it doesn't fit, we're done.
                if (points[4].Y > ymax)
                    break;
                else
                {
                    // Draw the row.
                    for (int col = 0; ; col++)
                    {
                        // Get the points for the row's next hexagon.
                        points = HexToPoints(height, row, col);

                        // If it doesn't fit horizontally,
                        // we're done with this row.
                        if (points[3].X > xmax) break;

                        foreach (PointF p in points)
                        {
                            result.Add(new PointF(p.X, p.Y));
                        };
                    }
                }
            }

            return RemoveDuplicates(result);
        }

        private List<PointF> RemoveDuplicates(List<PointF> points)
        {
            List<PointF> list = new List<PointF>();
            var request = points
                .Where(n1 => points.Where(n2 => n1.X == n2.X).Select(n3 => new { n1.X, n1.Y})
                .Where(n4 => points.Where(n5 => n4.Y == n5.Y).Select(n6 => new { n4.X, n4.Y })
            //var request = 
            //    points.Where(x 
            //    => points.Where(y => x.X == y.X)
            //    .Select(z => z.Y == x.Y).Select(n1 => new { x.X, x.Y})

                //for (var z = 0; z < points.Count; z++)
                //{
                //    var item = (from x in points
                //                 where x.X == points[z].X
                //                 where x.Y == points[z].Y
                //                 select x).First();
                //    result.Add(new PointF(item.X, item.Y));
                //}
            return list;
        }
    }
}

internal class PointF
{
    public float X;
    public float Y;

    public PointF(float x, float y)
    {
        this.X = x;
        this.Y = y;
    }
}