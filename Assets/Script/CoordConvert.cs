using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyMathTools
{
    [System.Serializable]
    public struct Polar
    {
        public float Rho;
        public float Theta;

        public Polar(Polar pol)
        {
            this.Rho = pol.Rho;
            this.Theta = pol.Theta;
        }

        public Polar(float rho, float theta)
        {
            this.Rho = rho;
            this.Theta = theta;
        }
    }
    [System.Serializable]
    public struct Spherical
    {
        public float Rho;
        public float Theta;
        public float Phi;

        public Spherical(Spherical pol)
        {
            this.Rho = pol.Rho;
            this.Theta = pol.Theta;
            this.Phi = pol.Phi;
        }

        public Spherical(float rho, float theta, float phi)
        {
            this.Rho = rho;
            this.Theta = theta;
            this.Phi = phi;
        }
        public Spherical Lerp(Spherical targetSph, Spherical lerpCoefs)
        {
            return new Spherical(Mathf.Lerp(this.Rho, targetSph.Rho, lerpCoefs.Rho),
                                 Mathf.Lerp(this.Theta, targetSph.Theta, lerpCoefs.Theta),
                                 Mathf.Lerp(this.Phi, targetSph.Phi, lerpCoefs.Phi));
        }
    }

    [System.Serializable]
    public struct Cylindrical
    {
        public float Rho;
        public float Theta;
        public float Y;

        public Cylindrical(Cylindrical cyl)
        {
            this.Rho = cyl.Rho;
            this.Theta = cyl.Theta;
            this.Y = cyl.Y;
        }

        public Cylindrical(float rho, float theta, float y)
        {
            this.Rho = rho;
            this.Theta = theta;
            this.Y = y;
        }
    }

    public static class CoordConvert
    {
        // -------------- 2D -------------- //
        public static Vector2 PolarToCartesian(Polar polar)
        {
            return polar.Rho * new Vector2(Mathf.Cos(polar.Theta), Mathf.Sin(polar.Theta));
        }

        public static Polar CartesianToPolar(Vector2 cart, bool keepThetaPositive = true)
        {
            Polar polar = new Polar(cart.magnitude, 0);
            if (Mathf.Approximately(polar.Rho, 0)) polar.Theta = 0;
            else
            {
                polar.Theta = Mathf.Atan2(cart.y, cart.x);
                if (keepThetaPositive && polar.Theta < 0) polar.Theta += 2 * Mathf.PI;
            }
            return polar;
        }

        // -------------- 3D -------------- //

        public static Vector3 CylindricalToCartesian(Cylindrical cyl)
        {
            return new Vector3(cyl.Rho * Mathf.Cos(cyl.Theta), cyl.Y, cyl.Rho * Mathf.Sin(cyl.Theta));
        }

        public static Vector3 SphericalToCartesian(Spherical sphere)
        {
            return sphere.Rho * new Vector3(Mathf.Sin(sphere.Phi) * Mathf.Cos(sphere.Theta), Mathf.Cos(sphere.Phi), Mathf.Sin(sphere.Phi) * Mathf.Sin(sphere.Theta));
        }

        public static Cylindrical CartesianToCylindrical(Vector3 cart, bool keepThetaPositive = true)
        {
            Cylindrical cyl = new Cylindrical(cart.magnitude, cart.y, 0);
            if (Mathf.Approximately(cyl.Rho, 0)) cyl.Theta = 0;
            else
            {
                cyl.Theta = Mathf.Atan2(cart.z, cart.x);
                if (keepThetaPositive && cyl.Theta < 0) cyl.Theta += 2 * Mathf.PI;
            }
            return cyl;
        }

        public static Spherical CartesianToSpherical(Vector3 cart, bool keepThetaPositive = true)
        {
            Spherical sphere = new Spherical(cart.magnitude, 0, 0);
            if (sphere.Rho <= 0) { sphere.Theta = 0; sphere.Phi = 0; }
            else
            {
                sphere.Phi = Mathf.Acos(cart.y / sphere.Rho);
                sphere.Theta = Mathf.Atan2(cart.z, cart.x);
                if (keepThetaPositive && sphere.Theta < 0) sphere.Theta += 2 * Mathf.PI;
            }

            return sphere;
        }

        // -------------- Additionnal methods -------------- //

        public static Cylindrical SphericalToCylindrical(Spherical sphere)
        {
            return new Cylindrical(sphere.Rho * Mathf.Sin(sphere.Phi), sphere.Theta, sphere.Rho * Mathf.Cos(sphere.Phi));
        }

        public static Spherical CylindricalToSpherical(Cylindrical cyl)
        {
            return new Spherical(Mathf.Sqrt(Mathf.Pow(cyl.Rho, 2) + Mathf.Pow(cyl.Y, 2)), cyl.Theta, Mathf.Atan(cyl.Rho / cyl.Y));
        }
    }
}