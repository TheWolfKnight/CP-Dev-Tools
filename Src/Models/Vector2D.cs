using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_Dev_Tools.Src.Models
{
    public class Vector2D
    {
        public double X { get; set; }
        public double Y { get; set; }

        /// <summary>
        /// Create a new Vector2D class with given parameters
        /// </summary>
        /// <param name="x"> X value for the vector </param>
        /// <param name="y"> Y value for the vector </param>
        public Vector2D( double x, double y )
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
            X /= i.X;
            Y /= i.Y;
        }


        public void Div( double n )
        {
            X /= n;
            Y /= n;
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


        /// <summary>
        /// Normalizes the Vector2D
        /// </summary>
        public void Norm()
        {
            double h = this.Mag();
            Div(h);
        }

    }
}
