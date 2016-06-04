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
using PhysicXNA.Nucleo;

namespace PhysicXNA.Niveles.Nivel5
{
    /// <summary>
    /// Proyectil que puede ser lanzado desde la nave.
    /// </summary>
    public class Proyectil : DuendeEstatico, iGravitatorio
    {
        double fuerza;
        double anguloFuerza;
        double masa;
        Vector2 aceleracion;
        Vector2 velocidad;
        internal Vector2 posicionAbsoluta;
        Vector2 dx;
        double velocidadTotal;
        Nivel5 nivelOrigen;
        internal Circulo[] area;

        /// <summary>
        /// Construye un nuevo proyectil para el escenario.
        /// </summary>
        /// <param name="nivelOrigen">Nivel en el que se colocará el proyectil.</param>
        /// <param name="posicionAbsoluta">Posición en la que se colocará el proyectil de todo el escenario.</param>
        /// <param name="angulo">Ángulo de dirección.</param>
        public Proyectil(Nivel5 nivelOrigen, Vector2 posicionAbsoluta, double angulo) :
            base("Recursos/Nivel5/laser", Vector2.Zero, escala: new Vector2(1f))
        {
            this.dx = Vector2.Zero;
            this.masa = 100;
            this.anguloFuerza = 0;
            this.velocidadTotal = 100;
            this.velocidad = new Vector2((float)(Math.Cos(angulo) * velocidadTotal + nivelOrigen.nave.velocidad.X), (float)(Math.Sin(angulo) * velocidadTotal + nivelOrigen.nave.velocidad.Y));
            this.posicionAbsoluta = posicionAbsoluta;
            this.AnguloGiro = (float)angulo;
            this.nivelOrigen = nivelOrigen;
            this.area = new Circulo[3];
        }

        /// <summary>
        /// Actualiza las posiciones absoluta y relativa del proyectil en el escenario y la pantalla.
        /// </summary>
        /// <param name="tiempoJuego">Tiempo del juego.</param>
        /// <param name="graficos">Dimensión de pantalla.</param>
        /// <param name="teclado">Estado actual del teclado.</param>
        public override void Actualizar(GameTime tiempoJuego, Viewport graficos, KeyboardState teclado)
        {
            posicionAbsoluta += (velocidad * tiempoJuego.ElapsedGameTime.Milliseconds / 1000f + dx) * Nivel5.pixelesXMetro;
            Posicion = new Vector2(posicionAbsoluta.X - nivelOrigen.nave.posicionAbsoluta.X + nivelOrigen.nave.Posicion.X, posicionAbsoluta.Y - nivelOrigen.nave.posicionAbsoluta.Y + nivelOrigen.nave.Posicion.Y);
            dx = Vector2.Zero;
            area[0] = new Circulo(posicionAbsoluta + new Vector2(Alto / 2f * (float)Math.Cos(AnguloGiro), Alto / 2f * (float)Math.Sin(AnguloGiro)), Alto / 2f);
            area[1] = new Circulo(posicionAbsoluta + new Vector2(Ancho / 2f * (float)Math.Cos(AnguloGiro), Ancho / 2f * (float)Math.Sin(AnguloGiro)), Alto / 2f);
            area[2] = new Circulo(posicionAbsoluta + new Vector2((Ancho - Alto / 2f) * (float)Math.Cos(AnguloGiro), (Ancho - Alto / 2f) * (float)Math.Sin(AnguloGiro)), Alto / 2f);

            base.Actualizar(tiempoJuego, graficos, teclado);
        }

        /// <summary><seealso cref="iGravitatorio.Masa"/></summary>
        public double Masa
        {
            get { return masa; }
            set { masa = value; }
        }

        /// <summary><seealso cref="iGravitatorio.Fuerza"/></summary>
        public double Fuerza
        {
            get { return fuerza; }
            set { fuerza = value; }
        }

        /// <summary><seealso cref="iGravitatorio.AnguloFuerza"/></summary>
        public double AnguloFuerza
        {
            get { return anguloFuerza; }
            set { anguloFuerza = value; }
        }

        /// <summary><seealso cref="iGravitatorio.Aceleracion"/></summary>
        public Vector2 Aceleracion
        {
            get { return aceleracion; }
            set { aceleracion = value; }
        }

        /// <summary><seealso cref="iGravitatorio.Velocidad"/></summary>
        public Vector2 Velocidad
        {
            get { return velocidad; }
            set { velocidad = value; }
        }

        /// <summary><seealso cref="iGravitatorio.PosicionAbsoluta"/></summary>
        public Vector2 PosicionAbsoluta
        {
            get { return posicionAbsoluta; }
            set { posicionAbsoluta = value; }
        }

        /// <summary><seealso cref="iGravitatorio.IncrementoPosicion"/></summary>
        public Vector2 IncrementoPosicion
        {
            get { return dx; }
            set { dx = value; }
        }

        /// <summary><seealso cref="iGravitatorio.Tamaño"/></summary>
        public Vector2 Tamaño
        {
            get { return new Vector2(Ancho, Alto); }
        }

        /// <summary><seealso cref="iGravitatorio.ObtenerIncrementoPosicion"/></summary>
        public void ObtenerIncrementoPosicion(iGravitatorio objeto, TimeSpan tiempoTranscurrido)
        {
            if (objeto.Equals(this)) return;
            double distancia = Math.Sqrt((objeto.PosicionAbsoluta.X - PosicionAbsoluta.X) * (objeto.PosicionAbsoluta.X - PosicionAbsoluta.X) + (objeto.PosicionAbsoluta.Y - PosicionAbsoluta.Y) * (objeto.PosicionAbsoluta.Y - PosicionAbsoluta.Y));
            Fuerza = Nivel5.CGU * Masa * objeto.Masa / (distancia * distancia);
            AnguloFuerza = Math.Atan2(objeto.PosicionAbsoluta.Y - PosicionAbsoluta.Y, objeto.PosicionAbsoluta.X - PosicionAbsoluta.X);
            Aceleracion = new Vector2((float)(Math.Cos(AnguloFuerza) * Fuerza / Masa), (float)(Math.Sin(AnguloFuerza) * Fuerza / Masa));
            Velocidad += Aceleracion * tiempoTranscurrido.Milliseconds / 1000f;
            IncrementoPosicion += Velocidad * tiempoTranscurrido.Milliseconds / 1000f;
        }
    }
}
