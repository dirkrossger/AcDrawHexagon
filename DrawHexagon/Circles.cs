using DrawHexagon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Circle
{
    private static float DEFAULT_RADIUS = 4;

    private readonly float x;
    private readonly float y;
    private float r;
    private bool growing;
    private readonly bool fill;
    private readonly int width;
    private readonly int height;
    private readonly float growSpeed;
    private float v1;
    private float v2;
    private float v3;
    private float v4;

    private Circle()
    {
        r = DEFAULT_RADIUS;
        growing = true;
    }

    public Circle(float x, float y, bool fill, int max_width, int max_height)
    {
        this.x = x;
        this.y = y;
        this.fill = fill;
        this.width = max_width;
        this.height = max_height;
    }

    public Circle(float v1, float v2, float v3, float v4)
    {
        this.v1 = v1;
        this.v2 = v2;
        this.v3 = v3;
        this.v4 = v4;
    }

    // Draw the circel
    public List<PointF> DrawCircleGrid(float xmin, float xmax, float ymin, float ymax, float radius)
    {
        List<PointF> points = new List<PointF>();
        //float[,] result = new float[,] { };

        // Loop until a hexagon won't fit.
        for (int row = 0; ; row++)
        {
            //// Get the points for the row's first hexagon.
            points.Add(new PointF(xmin + radius, ymin + radius));

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

    }

    // Increase the radius
    public void Grow()
    {
        if (growing)
        {
            if (AtEdge())
                growing = false;
            else
                r += growSpeed;
        }
    }

    // Checks for a collision with the other circle
    public bool IsCollision(Circle other)
    {
        if (other != this)
        {
            float deltaX = Math.Abs(this.x - other.x);
            float deltaY = Math.Abs(this.y - other.y);
            double dist = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));

            if (dist - 1 < this.r + other.r)
            {
                this.growing = false;
                return true;
            }
        }
        return false;
    }

    // Checks if the circel is hitting the edge
    private bool AtEdge()
    {
        return x + r > width - 1 || x - r < 0 || y + r > height - 1 || y - r < 0;
    }

}


