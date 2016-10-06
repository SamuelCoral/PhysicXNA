using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PhysicXNA.Nucleo;

namespace PhysicXNA.Niveles.Nivel2
{
    /// <summary>Componentes del HUD del nivel 2.</summary>
    public class Hud2
    {
        private Texto3D[] fuenteEnemigos;
        /// <summary>Texto de las coordenadas de los enemigos.</summary>
        public Texto3D[] FuenteEnemigos
        {
            get { return fuenteEnemigos; }
            set { fuenteEnemigos = value; }
        }

        private Texto3D posCursor;
        /// <summary>Texto de las coordenadas del cursor.</summary>
        public Texto3D PosCursor
        {
            get { return posCursor; }
            set { posCursor = value; }
        }

        private Texto3D Hud;
        /// <summary>Texto de las indicaciones del nivel.</summary>
        public Texto3D HUD
        {
            get { return Hud; }
            set { Hud = value; }
        }

        private Texto3D anotaciones;
        /// <summary>Texto que indica la puntuación.</summary>
        public Texto3D Anotaciones
        {
            get { return anotaciones; }
            set { anotaciones = value; }
        }

        private DuendeEstatico barraSuperior;
        /// <summary>Barra superior donde aparecerá el texto de las indicaciones.</summary>
        public DuendeEstatico BarraSuperior
        {
            get { return barraSuperior; }
            set { barraSuperior = value; }
        }

        private DuendeEstatico barraInferior;
        /// <summary>Barra donde aparecerán las coordenadas de los enemigos.</summary>
        public DuendeEstatico BarraInferior
        {
            get { return barraInferior; }
            set { barraInferior = value; }
        }

        private Texto3D tiempo;
        /// <summary>Texto del tiempo transcurrido del nivel.</summary>
        public Texto3D Tiempo
        {
            get { return tiempo; }
            set { tiempo = value; }
        }

        Nivel2 nivelOrigen;
        
        /// <summary>Constructor de esta clase.</summary>
        public Hud2()
        {
            this.Hud = new Texto3D("Fuentes/TextoMensaje", "Apunta a la fuerza opuesta.", Vector2.Zero, 1, new Color(172, 228, 255, 255), 5);
            this.anotaciones = new Texto3D("Fuentes/TextoAnotaciones", "", Vector2.Zero, 30, Color.Red, 5);
            this.posCursor = new Texto3D("Fuentes/TextoPosicion", "", Vector2.Zero, 1, new Color(235, 250, 72, 255), 3);
            this.tiempo = new Texto3D("Fuentes/TextoTiempo", "", Vector2.Zero, 30, new Color(172, 228, 255, 255), 5);
            this.barraSuperior = new DuendeEstatico("Menu/BarraAyuda", Vector2.Zero);
            this.barraInferior = new DuendeEstatico("Menu/BarraAyuda", Vector2.Zero, new Color(172, 228, 255, 255), new Vector2(1f), SpriteEffects.FlipVertically);

            this.fuenteEnemigos = new Texto3D[5];
            //for (int c = 0; c < 5; c++) fuenteEnemigos[c] = new Texto3D("Fuentes/TextoPosicion", "", Vector2.Zero, 1, new Color(1f - (float)c / 4, (float)c / 4, 0), 2);
            fuenteEnemigos[0] = new Texto3D("Fuentes/TextoPosicion", "", Vector2.Zero, 1, new Color(9, 95, 240), 3);
            fuenteEnemigos[1] = new Texto3D("Fuentes/TextoPosicion", "", Vector2.Zero, 1, new Color(240, 5, 212), 3);
            fuenteEnemigos[2] = new Texto3D("Fuentes/TextoPosicion", "", Vector2.Zero, 1, new Color(240, 150, 5), 3);
            fuenteEnemigos[3] = new Texto3D("Fuentes/TextoPosicion", "", Vector2.Zero, 1, new Color(5, 240, 32), 3);
            fuenteEnemigos[4] = new Texto3D("Fuentes/TextoPosicion", "", Vector2.Zero, 1, new Color(0, 0, 0), 3);
        }

        /// <summary>
        /// Inicializa los elementos de este HUD.
        /// </summary>
        /// <param name="nivelOrigen"></param>
        public void Inicializar(Nivel2 nivelOrigen)
        {
            this.nivelOrigen = nivelOrigen;
            Hud.Posicion = new Vector2(200f, 10f);
            anotaciones.Texto = "0";
            anotaciones.Posicion = new Vector2(nivelOrigen.botonAyuda.Posicion.X - 30 - anotaciones.TamTexto.X, 10);
            posCursor.Posicion = new Vector2(nivelOrigen.resolucionPantalla.Width - 220, nivelOrigen.resolucionPantalla.Height - 35);
            tiempo.Posicion = new Vector2(20f, 10f);
            barraSuperior.Escala = barraInferior.Escala = new Vector2((float)nivelOrigen.resolucionPantalla.Width / barraSuperior.AnchoReal, 0.5f);
            barraInferior.Posicion = new Vector2(0, nivelOrigen.resolucionPantalla.Height - barraInferior.Alto);

            for (int c = 0; c < 5; c++) fuenteEnemigos[c].Posicion = new Vector2((c / 2) * 250 + (float)20, nivelOrigen.resolucionPantalla.Height + (c % 2) * 20 - (float)45);
        }

        /// <summary>Carga el contenido de los elementos de este HUD.</summary>
        public void CargarContenido(ContentManager contenido)
        {
            Hud.CargarContenido(contenido);
            anotaciones.CargarContenido(contenido);
            posCursor.CargarContenido(contenido);
            tiempo.CargarContenido(contenido);
            barraInferior.CargarContenido(contenido);
            barraSuperior.CargarContenido(contenido);
            for (int c = 0; c < 5; c++) fuenteEnemigos[c].CargarContenido(contenido);
        }

        /// <summary>Libera el contenido ocupado por los elementos de este HUD.</summary>
        public void LiberarContenido()
        {
            Hud.LiberarContenido();
            anotaciones.LiberarContenido();
            posCursor.LiberarContenido();
            tiempo.LiberarContenido();
            barraSuperior.LiberarContenido();
            barraInferior.LiberarContenido();
            for (int c = 0; c < 5; c++) fuenteEnemigos[c].LiberarContenido();
        }

        /// <summary>Actualiza los elementos de este HUD.</summary>
        /// <param name="tiempoJuego">Tiempo del juego.</param>
        public void Actualizar(GameTime tiempoJuego)
        {
            tiempo.Texto = nivelOrigen.tiempoCompletado.Minutes.ToString("00") + ":" + nivelOrigen.tiempoCompletado.Seconds.ToString("00");
            posCursor.Texto = "Tu: X = " + (nivelOrigen.cursor.Posicion.X - nivelOrigen.resolucionPantalla.Width / 2).ToString("0000") +
                "; Y = " + (-nivelOrigen.cursor.Posicion.Y + nivelOrigen.resolucionPantalla.Height / 2).ToString("0000") + ";";
            for (int c = 0; c < nivelOrigen.numEnem; c++) fuenteEnemigos[c].Texto = "E" + (c + 1).ToString() +
                 ": X = " + (nivelOrigen.enemigos[c].Posicion.X - nivelOrigen.resolucionPantalla.Width / 2).ToString("0000") +
                 "; Y = " + (-nivelOrigen.enemigos[c].Posicion.Y + nivelOrigen.resolucionPantalla.Height / 2).ToString("0000") + ";";
            if (nivelOrigen.tiempoMalo >= TimeSpan.Zero) { tiempo.Actualizar(tiempoJuego); nivelOrigen.tiempoMalo -= tiempoJuego.ElapsedGameTime; }
            else { tiempo.Color = Color.Yellow; tiempo.Reiniciar(); }
            if (nivelOrigen.anotacion >= TimeSpan.Zero) { anotaciones.Actualizar(tiempoJuego); nivelOrigen.anotacion -= tiempoJuego.ElapsedGameTime; }
            else { anotaciones.Color = Color.Red; anotaciones.Reiniciar(); }
        }

        /// <summary>Dibuja los elementos de este HUD.</summary>
        public void Dibujar(SpriteBatch spriteBatch, GameTime gameTime)
        {
            barraInferior.Dibujar(spriteBatch, gameTime);
            Hud.Dibujar(spriteBatch, gameTime);
            anotaciones.Dibujar(spriteBatch, gameTime);
            posCursor.Dibujar(spriteBatch, gameTime);
            tiempo.Dibujar(spriteBatch, gameTime);
            for (int c = 0; c < nivelOrigen.numEnem; c++) fuenteEnemigos[c].Dibujar(spriteBatch, gameTime);
        }
    }
}
