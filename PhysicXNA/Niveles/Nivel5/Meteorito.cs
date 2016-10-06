using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhysicXNA.Nucleo;

namespace PhysicXNA.Niveles.Nivel5
{
    /// <summary>
    /// Meteorito colocado en el escenario del nivel 5.
    /// </summary>
    public class Meteorito : DuendeEstatico, iGravitatorio
    {
        double masa;
        Vector2 dx;
        Vector2 posicionAbsoluta;
        double fuerza;
        double anguloFuerza;
        Vector2 aceleracion;
        Vector2 velocidad;
        /// <summary>Vida restante del meteorito, cuando llega a 0 el meteorito debe ser destruido.</summary>
        public int vida;
        Nivel5 nivelOrigen;
        internal DuendeEstatico flecha;
        internal bool dibujarFlecha;
        internal Circulo area;

        /// <summary>
        /// Construye un nuevo meteorito para el nivel dado.
        /// </summary>
        /// <param name="nivelOrigen">Nivel a colocar el meteorito.</param>
        public Meteorito(Nivel5 nivelOrigen)
            : base("Recursos/Nivel5/meteorito", Vector2.Zero)
        {
            this.nivelOrigen = nivelOrigen;
            dx = Vector2.Zero;
            anguloFuerza = 0;
            vida = 100;
            flecha = new DuendeEstatico("Recursos/Nivel5/flecha", Vector2.Zero, Color.Blue);
            dibujarFlecha = true;
        }

        /// <summary>
        /// Actualiza la posición del meteorito en el escenario y la flecha indicadora en caso de que esté perdido.
        /// </summary>
        /// <param name="tiempoJuego">Tiempo del juego.</param>
        /// <param name="graficos">Resolución de pantalla.</param>
        /// <param name="teclado">Estado del teclado.</param>
        public override void Actualizar(GameTime tiempoJuego, Viewport graficos, KeyboardState teclado)
        {
            PosicionAbsoluta += dx * Nivel5.pixelesXMetro;
            area = new Circulo(PosicionAbsoluta + new Vector2(Ancho / 2, Alto / 2), Ancho / 2);
            Posicion = new Vector2(posicionAbsoluta.X - nivelOrigen.nave.posicionAbsoluta.X + nivelOrigen.nave.Posicion.X, posicionAbsoluta.Y - nivelOrigen.nave.posicionAbsoluta.Y + nivelOrigen.nave.Posicion.Y);
            dibujarFlecha = false;
            if (Posicion.X + Ancho < 0)
            {
                dibujarFlecha = true;
                flecha.Posicion = new Vector2(0, Posicion.Y + Alto / 2f);
                flecha.AnguloGiro = (float)Math.PI;
            }
            if (Posicion.X > graficos.Width)
            {
                dibujarFlecha = true;
                flecha.Posicion = new Vector2(graficos.Width, Posicion.Y + Alto / 2f);
                flecha.AnguloGiro = 0f;
            }
            if (Posicion.Y + Alto < 0)
            {
                dibujarFlecha = true;
                flecha.Posicion = new Vector2(Posicion.X + Ancho / 2f, 0);
                flecha.AnguloGiro = (float)(-Math.PI / 2d);
            }
            if (Posicion.Y > graficos.Height)
            {
                dibujarFlecha = true;
                flecha.Posicion = new Vector2(Posicion.X + Ancho / 2f, graficos.Height);
                flecha.AnguloGiro = (float)(Math.PI / 2d);
            }
            if (dibujarFlecha) flecha.Posicion = new Vector2(
                flecha.Posicion.X < flecha.Alto / 2f ? flecha.Alto / 2f : flecha.Posicion.X > graficos.Width - flecha.Alto / 2f ? graficos.Width - flecha.Alto / 2f : flecha.Posicion.X,
                flecha.Posicion.Y < flecha.Ancho / 2f ? flecha.Ancho / 2f : flecha.Posicion.Y > graficos.Height - flecha.Ancho / 2f ? graficos.Height - flecha.Ancho / 2f : flecha.Posicion.Y);
            dx = Vector2.Zero;

            base.Actualizar(tiempoJuego, graficos, teclado);
        }

        /// <summary><see cref="iGravitatorio.Masa"/></summary>
        public double Masa
        {
            get { return masa; }
            set { masa = value; }
        }

        /// <summary><see cref="iGravitatorio.Fuerza"/></summary>
        public double Fuerza
        {
            get { return fuerza; }
            set { fuerza = value; }
        }

        /// <summary><see cref="iGravitatorio.AnguloFuerza"/></summary>
        public double AnguloFuerza
        {
            get { return anguloFuerza; }
            set { anguloFuerza = value; }
        }

        /// <summary><see cref="iGravitatorio.Aceleracion"/></summary>
        public Vector2 Aceleracion
        {
            get { return aceleracion; }
            set { aceleracion = value; }
        }

        /// <summary><see cref="iGravitatorio.Velocidad"/></summary>
        public Vector2 Velocidad
        {
            get { return velocidad; }
            set { velocidad = value; }
        }

        /// <summary><see cref="iGravitatorio.PosicionAbsoluta"/></summary>
        public Vector2 PosicionAbsoluta
        {
            get { return posicionAbsoluta; }
            set { posicionAbsoluta = value; }
        }

        /// <summary><see cref="iGravitatorio.IncrementoPosicion"/></summary>
        public Vector2 IncrementoPosicion
        {
            get { return dx; }
            set { dx = value; }
        }

        /// <summary><see cref="iGravitatorio.Tamaño"/></summary>
        public Vector2 Tamaño
        {
            get { return new Vector2(Ancho, Alto); }
        }

        /// <summary><see cref="iGravitatorio.ObtenerIncrementoPosicion"/></summary>
        public void ObtenerIncrementoPosicion(iGravitatorio objeto, TimeSpan tiempoTranscurrido)
        {
            if (objeto.Equals(this)) return;
            Vector2 deltaDistancia = new Vector2((objeto.PosicionAbsoluta.X + objeto.Tamaño.X / 2) - (PosicionAbsoluta.X + Ancho / 2f), (objeto.PosicionAbsoluta.Y + objeto.Tamaño.Y / 2) - (PosicionAbsoluta.Y + Alto / 2f));
            double distancia = Math.Sqrt(deltaDistancia.X * deltaDistancia.X + deltaDistancia.Y * deltaDistancia.Y);
            Fuerza = Nivel5.CGU * Masa * objeto.Masa / (distancia * distancia);
            AnguloFuerza = Math.Atan2(deltaDistancia.Y, deltaDistancia.X);
            Aceleracion = new Vector2((float)(Math.Cos(AnguloFuerza) * Fuerza / Masa), (float)(Math.Sin(AnguloFuerza) * Fuerza / Masa));
            Velocidad += Aceleracion * tiempoTranscurrido.Milliseconds / 1000f;
            IncrementoPosicion += Velocidad * tiempoTranscurrido.Milliseconds / 1000f;
        }
    }
}
