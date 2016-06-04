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

namespace PhysicXNA.Niveles.Nivel3
{
    /// <summary>Escenario del Nivel 3.</summary>
    public class Nivel3 : Nivel
    {
        SoundEffect bien;
        SoundEffect mal;
        SoundEffect disparo;

        DuendeEstatico fondoTierra;
        DuendeEstatico fondoLuna;
        DuendeEstatico fondoNeptuno;
        DuendeEstatico canasta;
        DuendeEstatico[] pelota;
        DuendeEstatico vector;
        Cañon cañon;

        Texto3D tiempo;
        Texto3D mensaje;
        Texto3D anotaciones;
        DuendeEstatico cursor;

        TimeSpan tiempoMalo;
        TimeSpan Anotacion;
        Random genNumAleatorios = new Random();
        int vecesRepetido;
        int vecesCompletar;
        Vector2 posAnt = Vector2.Zero;

        bool aventada = false;
        int tiempoSalto;
        Vector2 vi;
        Vector2 delta;
        float gravedad = 0.5f;
        Vector2 velocidad;
        Vector2 pixelesXMetro = new Vector2(200f);

        /// <summary>
        /// Construye una instancia para el nivel 3.
        /// </summary>
        /// <param name="juego">Juego en que será mostrado el nivel.</param>
        /// <param name="rutaVideo">Ruta del archivo de video de introducción al nivel.</param>
        /// <param name="rutaMusica">Ruta del archivo de música que será reproducido en el nivel.</param>
        /// <param name="cuadrosPorSegundo"><seealso cref="Nivel.cuadrosPorSegundo"/></param>
        public Nivel3(Game juego, String rutaMusica, String rutaVideo, int cuadrosPorSegundo)
            : base(juego, rutaMusica, rutaVideo, cuadrosPorSegundo)
        {
            reglas[0] =
                "Apunta el proyectil para lanzarlo dentro del objetivo\n" +
                "Cuidado con la gravedad del planeta, si no anotas,\n" +
                "serás penalizado con tiempo.";
            reglas[1] =
                "Click pulsado - Apuntar el proyectil con fuerza y dirección\n" + 
                "Click soltado - Disparar el proyectil";
            reglas[2] =
                "                                                  /* Tiro parabólico*\\\n" + 
                "El tiro parabólico es un movimiento natural relacionado directamente\n" +
                "con la fuerza de atracción entre 2 cuerpos por efecto de la gravedad.\n" + 
                "En física es muy importante su estudio además de que se vive y\n" + 
                "experimenta en la vida diaria.";

            fondoTierra = new DuendeEstatico("Fondos/fondo1", Vector2.Zero);
            fondoLuna = new DuendeEstatico("Fondos/fondo4", Vector2.Zero);
            fondoNeptuno = new DuendeEstatico("Fondos/fondo3", Vector2.Zero);

            tiempo = new Texto3D("Fuentes/TextoTiempo", String.Empty, new Vector2(10f, 10f), 30, Color.Yellow, 3);
            mensaje = new Texto3D("Fuentes/TextoMensaje", "Apunta bien el proyectil para lanzarlo al objetivo.", new Vector2(10, 50), 30, new Color(172, 228, 255, 255), 3);
            anotaciones = new Texto3D("Fuentes/TextoAnotaciones", String.Empty, Vector2.Zero, 30, Color.Red, 3);
            cursor = new DuendeEstatico("Cursores/Cursor", Vector2.Zero, Color.White, new Vector2(0.1f));

            pelota = new DuendeEstatico[3];
            canasta = new DuendeEstatico("Recursos/Nivel3/canasta", Vector2.Zero);
            pelota[0] = new DuendeEstatico("Recursos/Nivel3/esfera1", Vector2.Zero, escala: new Vector2(0.25f));
            pelota[1] = new DuendeEstatico("Recursos/Nivel3/esfera2", Vector2.Zero, escala: new Vector2(0.25f));
            pelota[2] = new DuendeEstatico("Recursos/Nivel3/esfera3", Vector2.Zero, escala: new Vector2(0.25f));
            vector = new DuendeEstatico("Recursos/Nivel3/vector", Vector2.Zero);
            cañon = new Cañon(1000);
        }

        /// <summary><seealso cref="Nivel.LoadContent"/></summary>
        protected override void LoadContent()
        {
            bien = Content.Load<SoundEffect>("Sonidos/bien");
            mal = Content.Load<SoundEffect>("Sonidos/mal");
            disparo = Content.Load<SoundEffect>("Sonidos/Salto");
            fondoTierra.CargarContenido(Content);
            fondoLuna.CargarContenido(Content);
            fondoNeptuno.CargarContenido(Content);
            tiempo.CargarContenido(Content);
            mensaje.CargarContenido(Content);
            anotaciones.CargarContenido(Content);
            cursor.CargarContenido(Content);
            canasta.CargarContenido(Content);
            foreach(DuendeEstatico pelotita in pelota) pelotita.CargarContenido(Content);
            vector.CargarContenido(Content);
            cañon.CargarContenido(Content);
          
            base.LoadContent();
        }

        /// <summary><seealso cref="Nivel.UnloadContent"/></summary>
        protected override void UnloadContent()
        {
            bien.Dispose();
            mal.Dispose();
            disparo.Dispose();
            fondoTierra.LiberarContenido();
            fondoLuna.LiberarContenido();
            fondoNeptuno.LiberarContenido();
            canasta.LiberarContenido();
            foreach (DuendeEstatico pelotita in pelota) pelotita.LiberarContenido();
            vector.LiberarContenido();
            tiempo.LiberarContenido();
            mensaje.LiberarContenido();
            anotaciones.LiberarContenido();
            cursor.LiberarContenido();
            cañon.LiberarContenido();

            base.UnloadContent();
        }

        private void Reiniciar()
        {
            cañon.disparando = aventada = false;
            pelota[(int)opciones.dificultad].Posicion = vector.Posicion = new Vector2(genNumAleatorios.Next((resolucionPantalla.Width - cañon.Ancho) / 2) + cañon.Ancho / 2f,
                genNumAleatorios.Next(resolucionPantalla.Height * 2 / 3, resolucionPantalla.Height) - cañon.Ancho / 2f);
            cañon.AnguloGiro = 0f;
            cañon.Posicion = pelota[(int)opciones.dificultad].Posicion;
        }

        /// <summary><seealso cref="Nivel.Inicializar"/></summary>
        public override void Inicializar()
        {
            base.Inicializar();
            switch (opciones.dificultad)
            {
                case SistemaPerfiles.DificultadJuego.Fácil: dibujarAntes.Add(fondoTierra); break;
                case SistemaPerfiles.DificultadJuego.Medio: dibujarAntes.Add(fondoLuna); break;
                case SistemaPerfiles.DificultadJuego.Difícil: dibujarAntes.Add(fondoNeptuno); break;
            }

            Anotacion = TimeSpan.Zero;
            vecesRepetido = 0;
            vecesCompletar = opciones.dificultad == SistemaPerfiles.DificultadJuego.Fácil ? 7 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Medio ? 10 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Difícil ? 15 : 10;
            gravedad = opciones.dificultad == SistemaPerfiles.DificultadJuego.Fácil ? 9.8f : opciones.dificultad == SistemaPerfiles.DificultadJuego.Medio ? 2f : opciones.dificultad == SistemaPerfiles.DificultadJuego.Difícil ? 18f : 100;

            pelota[(int)opciones.dificultad].OrigenGiro = new Vector2(pelota[(int)opciones.dificultad].AnchoReal / 2, pelota[(int)opciones.dificultad].AltoReal / 2);
            vector.OrigenGiro = new Vector2(0f, (float)vector.AltoReal / 2);
            canasta.Escala = new Vector2(0.40f) * (resolucionPantalla.Width / 800f) * (1.2f - (int)opciones.dificultad * 0.2f);
            canasta.Posicion = new Vector2(resolucionPantalla.Width - canasta.Ancho, resolucionPantalla.Height / 3);
            cañon.OrigenGiro = new Vector2(cañon.AnchoReal / 2f, cañon.AltoReal / 2f);

            anotaciones.Texto = "0";
            anotaciones.Posicion = new Vector2(botonAyuda.Posicion.X - 30 - anotaciones.TamTexto.X, 10);
            tiempoMalo = TimeSpan.Zero;

            fondoTierra.Escala = new Vector2((float)resolucionPantalla.Width / fondoTierra.AnchoReal, (float)resolucionPantalla.Height / fondoTierra.AltoReal);
            fondoLuna.Escala = new Vector2((float)resolucionPantalla.Width / fondoLuna.AnchoReal, (float)resolucionPantalla.Height / fondoLuna.AltoReal);
            fondoNeptuno.Escala = new Vector2((float)resolucionPantalla.Width / fondoNeptuno.AnchoReal, (float)resolucionPantalla.Height / fondoNeptuno.AltoReal);
            Reiniciar();
        }

        /// <summary><seealso cref="Nivel.Update"/></summary>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (reproduciendoVideo) return;

            cursor.Posicion = new Vector2(estadoRaton.X, estadoRaton.Y);
            tiempo.Texto = tiempoCompletado.Minutes.ToString("00") + ":" + tiempoCompletado.Seconds.ToString("00");

            cañon.Actualizar(gameTime, resolucionPantalla, estadoTeclado);
            if ((opciones.invertir_mouse == 0 ? estadoRaton.LeftButton == ButtonState.Pressed : estadoRaton.RightButton == ButtonState.Pressed) && !aventada)
            {
                cañon.AnguloGiro = vector.AnguloGiro = (float)Math.Atan2((estadoRaton.Y - vector.Posicion.Y), (estadoRaton.X - vector.Posicion.X));
                vector.Escala = new Vector2((float)Math.Sqrt((estadoRaton.X - vector.Posicion.X) * (estadoRaton.X - vector.Posicion.X) + (estadoRaton.Y - vector.Posicion.Y) * (estadoRaton.Y - vector.Posicion.Y)) / vector.AnchoReal, 0.25f);
            }

            if ((opciones.invertir_mouse == 0 ? estadoRaton.LeftButton == ButtonState.Released && estadoRatonAnt.LeftButton == ButtonState.Pressed :
                estadoRaton.RightButton == ButtonState.Released && estadoRatonAnt.RightButton == ButtonState.Pressed) && !aventada)
            {
                cañon.disparando = aventada = true;
                //vi = new Vector2((float)Math.Cos(vector.AnguloGiro) * vector.Ancho / 30, -(float)Math.Sin(vector.AnguloGiro) * vector.Ancho / 30);
                double velocidadA = Math.Sqrt((2 * (double)vector.Ancho) / pixelesXMetro.Y * 2 * gravedad);
                vi = new Vector2((float)(Math.Cos(vector.AnguloGiro) * velocidadA), -(float)(Math.Sin(vector.AnguloGiro) * velocidadA));
                tiempoSalto = 0;
                if (opciones.sonidos != 0) disparo.Play(0.25f, 0f, 0f);
            }

            if (opciones.invertir_mouse == 0 ? estadoRaton.LeftButton == ButtonState.Released : estadoRaton.RightButton == ButtonState.Released) vector.Escala = Vector2.Zero;

            if (aventada)
            {
                tiempoSalto += gameTime.ElapsedGameTime.Milliseconds;
                velocidad = new Vector2(vi.X, vi.Y - gravedad * ((float)tiempoSalto / 1000));
                delta.Y = velocidad.Y * (gameTime.ElapsedGameTime.Milliseconds / 1000f) * pixelesXMetro.Y;
                delta.X = velocidad.X * (gameTime.ElapsedGameTime.Milliseconds / 1000f) * pixelesXMetro.X;
                pelota[(int)opciones.dificultad].Posicion = new Vector2(pelota[(int)opciones.dificultad].Posicion.X + delta.X, pelota[(int)opciones.dificultad].Posicion.Y - delta.Y);

                if (pelota[(int)opciones.dificultad].Posicion.X > resolucionPantalla.Width || pelota[(int)opciones.dificultad].Posicion.X < 0 ||
                    pelota[(int)opciones.dificultad].Posicion.Y > resolucionPantalla.Height)
                {
                    tiempoCompletado += new TimeSpan(0, 0, opciones.dificultad == SistemaPerfiles.DificultadJuego.Fácil ? 5 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Medio ? 10 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Difícil ? 15 : 0);
                    tiempoMalo = new TimeSpan(0, 0, 3);
                    tiempo.Color = Color.Red;
                    if (opciones.sonidos != 0) mal.Play();
                    Reiniciar();
                }

                if (pelota[(int)opciones.dificultad].Posicion.X > canasta.Posicion.X && pelota[(int)opciones.dificultad].Posicion.Y > canasta.Posicion.Y + canasta.Alto * 0.22f &&
                    posAnt.X > canasta.Posicion.X && posAnt.Y < canasta.Posicion.Y + canasta.Alto * 0.22f)
                {
                    vecesRepetido++;
                    anotaciones.Texto = vecesRepetido.ToString();
                    Anotacion = new TimeSpan(0, 0, 3);
                    anotaciones.Color = Color.Gold;
                    if (opciones.sonidos != 0) bien.Play();
                    if (vecesRepetido == vecesCompletar) nivelCompletado = true;
                    Reiniciar();
                }

                posAnt = pelota[(int)opciones.dificultad].Posicion;
            }

            if (tiempoMalo >= TimeSpan.Zero) { tiempo.Actualizar(gameTime); tiempoMalo -= gameTime.ElapsedGameTime; }
            else { tiempo.Color = Color.Yellow; tiempo.Reiniciar(); }
            if (Anotacion >= TimeSpan.Zero) { anotaciones.Actualizar(gameTime); Anotacion -= gameTime.ElapsedGameTime; }
            else { anotaciones.Color = Color.Red; anotaciones.Reiniciar(); }

            estadoTecladoAnt = estadoTeclado;
            estadoRatonAnt = estadoRaton;
        }

        /// <summary><seealso cref="Nivel.Draw"/></summary>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (reproduciendoVideo) return;
            cañon.Dibujar(spriteBatch, gameTime);
            vector.Dibujar(spriteBatch, gameTime);
            canasta.Dibujar(spriteBatch, gameTime);
            if (cañon.disparando) pelota[(int)opciones.dificultad].Dibujar(spriteBatch, gameTime);
            
            tiempo.Dibujar(spriteBatch, gameTime);
            mensaje.Dibujar(spriteBatch, gameTime);
            anotaciones.Dibujar(spriteBatch, gameTime);
            cursor.Dibujar(spriteBatch, gameTime);
          
            spriteBatch.End();
        }
    }
}
