using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhysicXNA.Nucleo;

namespace PhysicXNA.Niveles.Nivel5
{
    /// <summary>
    /// Nave espacial para el nivel 5.
    /// </summary>
    public class NaveEspacial : DuendeAnimado, iGravitatorio
    {
        /// <summary>Posición en el escenario entero.</summary>
        public Vector2 posicionAbsoluta;
        Vector2 aceleracion;
        /// <summary>Velocidad de la nave en el escenario.</summary>
        public Vector2 velocidad;
        internal Circulo area;

        internal DuendeEstatico flecha;
        Nivel5 escenario;

        double masa;
        double fuerzaMovimiento;
        double anguloFuerza;
        Vector2 dx;
        bool avanzando;
        internal bool dibujarFlecha;

        /// <summary>
        /// Construye una nave espacial para el nivel 5.
        /// </summary>
        /// <param name="escenario">Nivel en el que se dibujará la nave.</param>
        public NaveEspacial(Nivel5 escenario) :
            base("Recursos/Nivel5/nave", Vector2.Zero, new Point(100, 100), new Point(6, 1), 6, escala: new Vector2(0.5f))
        {
            this.escenario = escenario;
            masa = 20000;
            anguloFuerza = 0;
            dx = Vector2.Zero;
            avanzando = false;
            flecha = new DuendeEstatico("Recursos/Nivel5/flecha", Vector2.Zero, Color.Red, new Vector2(0.5f));
            dibujarFlecha = false;
        }

        /// <summary>
        /// Actualiza la posición, velocidad y aceleración de la nave en el escenario.
        /// </summary>
        /// <param name="tiempoJuego">Tiempo del juego.</param>
        /// <param name="graficos">Resolución de pantalla.</param>
        /// <param name="teclado">Estado actual del teclado.</param>
        /// <param name="estadoRaton">Estado del ratón.</param>
        public void Actualizar(GameTime tiempoJuego, Viewport graficos, KeyboardState teclado, MouseState estadoRaton)
        {
            anguloFuerza = escenario.opciones.wasd != 0 ? teclado.IsKeyDown(Keys.W) ? teclado.IsKeyDown(Keys.D) ? -Math.PI / 4 : -Math.PI / 2 :
                teclado.IsKeyDown(Keys.D) ? teclado.IsKeyDown(Keys.S) ? Math.PI / 4 : 0 :
                teclado.IsKeyDown(Keys.S) ? teclado.IsKeyDown(Keys.A) ? Math.PI * 3 / 4 : Math.PI / 2 :
                teclado.IsKeyDown(Keys.A) ? teclado.IsKeyDown(Keys.W) ? -Math.PI * 3 / 4 : Math.PI : 0 :
                teclado.IsKeyDown(Keys.Up) ? teclado.IsKeyDown(Keys.Right) ? -Math.PI / 4 : -Math.PI / 2 :
                teclado.IsKeyDown(Keys.Right) ? teclado.IsKeyDown(Keys.Down) ? Math.PI / 4 : 0 :
                teclado.IsKeyDown(Keys.Down) ? teclado.IsKeyDown(Keys.Left) ? Math.PI * 3 / 4 : Math.PI / 2 :
                teclado.IsKeyDown(Keys.Left) ? teclado.IsKeyDown(Keys.Up) ? -Math.PI * 3 / 4 : Math.PI : 0;
            avanzando = escenario.opciones.wasd != 0 ? teclado.IsKeyDown(Keys.W) || teclado.IsKeyDown(Keys.A) || teclado.IsKeyDown(Keys.S) || teclado.IsKeyDown(Keys.D) :
                teclado.IsKeyDown(Keys.Up) || teclado.IsKeyDown(Keys.Left) || teclado.IsKeyDown(Keys.Down) || teclado.IsKeyDown(Keys.Right);
            fuerzaMovimiento = 100000 * (teclado.IsKeyDown(Keys.RightShift) || teclado.IsKeyDown(Keys.LeftShift) || (escenario.opciones.invertir_mouse != 0 ? estadoRaton.LeftButton == ButtonState.Pressed : estadoRaton.RightButton == ButtonState.Pressed) ? 5 : 1) / (teclado.IsKeyDown(Keys.RightControl) || teclado.IsKeyDown(Keys.LeftControl) ? 2f : 1f);

            aceleracion = !avanzando ? Vector2.Zero : new Vector2((float)(Math.Cos(anguloFuerza) * fuerzaMovimiento / masa), (float)(Math.Sin(anguloFuerza) * fuerzaMovimiento / masa));
            velocidad += aceleracion * tiempoJuego.ElapsedGameTime.Milliseconds / 1000f;
            posicionAbsoluta += (velocidad * tiempoJuego.ElapsedGameTime.Milliseconds / 1000f + dx) * Nivel5.pixelesXMetro;
            dx = Vector2.Zero;

            Point mitadPantallaAbsoluta = new Point((escenario.fondo.Ancho - Ancho) / 2, (escenario.fondo.Alto - Alto) / 2);
            Point mitadPantallaRelativa = new Point((graficos.Width - Ancho) / 2, (graficos.Height - Alto) / 2);
            Posicion = new Vector2(posicionAbsoluta.X < mitadPantallaRelativa.X ? posicionAbsoluta.X : posicionAbsoluta.X > escenario.fondo.Ancho - (mitadPantallaRelativa.X + Ancho) ? posicionAbsoluta.X - (escenario.fondo.Ancho - graficos.Width) : mitadPantallaRelativa.X,
                posicionAbsoluta.Y < mitadPantallaRelativa.Y ? posicionAbsoluta.Y : posicionAbsoluta.Y > escenario.fondo.Alto - (mitadPantallaRelativa.Y + Alto) ? posicionAbsoluta.Y - (escenario.fondo.Alto - graficos.Height) : mitadPantallaRelativa.Y);
            escenario.fondo.Posicion = new Vector2(-(posicionAbsoluta.X - mitadPantallaRelativa.X), -(posicionAbsoluta.Y - mitadPantallaRelativa.Y));
            escenario.fondo.Posicion = new Vector2(Posicion.X < mitadPantallaRelativa.X ? 0 : Posicion.X > mitadPantallaRelativa.X ? -((float)escenario.fondo.Ancho - graficos.Width) : escenario.fondo.Posicion.X,
                Posicion.Y < mitadPantallaRelativa.Y ? 0 : Posicion.Y > mitadPantallaRelativa.Y ? -((float)escenario.fondo.Alto - graficos.Height) : escenario.fondo.Posicion.Y);

            area = new Circulo(PosicionAbsoluta + new Vector2(Ancho / 2f), Ancho / 2f);

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
            get { return fuerzaMovimiento; }
            set { fuerzaMovimiento = value; }
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
