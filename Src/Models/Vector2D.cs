using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_Dev_Tools.Src.Models
{
    public class Vector2D
    {
        public int X { get; set; }
        public int Y { get; set; }


        /// <summary>
        /// Creates a vector going to origo
        /// </summary>
        public Vector2D()
        {
            X = 0;
            Y = 0;
        }


        /// <summary>
        /// Create a new Vector2D class with given parameters
        /// </summary>
        /// <param name="x"> X value for the vector </param>
        /// <param name="y"> Y value for the vector </param>
        public Vector2D( int x, int y )
        {
            X = x;
            Y = y;
        }


        /// <summary>
        /// Subtracts the Vector2D class instance form another Vector2D class instance
        /// </summary>
        /// <param name="i"> The input vector </param>
        public void Minus( Vector2D i )
        {
            X -= i.X;
            Y -= i.Y;
        }


        /// <summary>
        /// Adds a Vector2D class instance to the caller instance
        /// </summary>
        /// <param name="i"> The input vector to be added </param>
        public void Plus( Vector2D i )
        {
            X += i.X;
            Y += i.Y;
        }


        /// <summary>
        /// Multiplys two vectors together
        /// </summary>
        /// <param name="i"> The input vector </param>
        public void Mult( Vector2D i )
        {
            X *= i.X;
            Y *= i.Y;
        }


        /// <summary>
        /// Diveds the Vector2D instance with another Vector2D instance
        /// </summary>
        /// <param name="i"> The diviser for this vector </param>
        public void Div( Vector2D i )
        {
            X = (int)Math.Floor( (double)X / i. X);
            Y = (int)Math.Floor( (double)Y / i.Y );
        }


        /// <summary>
        /// Gets the distance between two vectors using the pythagorian therom
        /// </summary>
        /// <param name="i"> The vector that the distance will be calcualted to </param>
        /// <returns> The distance between the two Vector2D's </returns>
        public double Dist( Vector2D i )
        {
            double x2 = X * X - i.X * i.X;
            double y2 = Y * Y - i.Y * i.Y;

            return Math.Sqrt(x2 + y2);

        }

        /// <summary>
        /// Gets the magnitude of the Vector2D;
        /// </summary>
        /// <returns> The magnitude of the Vector2D in the form of a double </returns>
        public double Mag()
        {
            double x = X * X;
            double y = Y * Y;

            return Math.Sqrt(x + y);
        }


        public override string ToString()
        {
            return $"Vector(X={X}, Y={Y})";
        }

    }
}
