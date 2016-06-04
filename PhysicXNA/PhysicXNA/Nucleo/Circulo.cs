using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PhysicXNA.Niveles.Nivel5
{
    /// <summary>
    /// Contiene métodos comunes para trabajar con círculos.
    /// </summary>
    public struct Circulo
    {
        /// <summary>Coordenadas del centro del círculo.</summary>
        public Vector2 centro;
        /// <summary>Radio del círculo.</summary>
        public float radio;

        /// <summary>
        /// Crea un nuevo círculo dado su centro y su radio.
        /// </summary>
        /// <param name="centro"><seealso cref="centro"/></param>
        /// <param name="radio"><seealso cref="radio"/></param>
        public Circulo(Vector2 centro, float radio)
        {
            this.centro = centro;
            this.radio = radio;
        }
        
        /// <summary>
        /// Indica si un punto está dentro del círculo.
        /// </summary>
        /// <param name="punto">Coordenadas del punto a probar.</param>
        /// <returns>Booleano que indica la condición.</returns>
        public bool Contiene(Vector2 punto)
        {
            return Math.Sqrt((punto.X - centro.X) * (punto.X - centro.X) + (punto.Y - centro.Y) * (punto.Y - centro.Y)) < radio;
        }

        /// <summary>
        /// Indica si un círculo se superpone con otro.
        /// </summary>
        /// <param name="otro">Círculo a comprobar la superposición con el primero.</param>
        /// <returns>Booleano que indica la condición.</returns>
        public bool Intersecta(Circulo otro)
        {
            return Math.Sqrt((otro.centro.X - centro.X) * (otro.centro.X - centro.X) + (otro.centro.Y - centro.Y) * (otro.centro.Y - centro.Y)) < radio + otro.radio;
        }
    }
}
