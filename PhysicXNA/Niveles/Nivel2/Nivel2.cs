using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using PhysicXNA.Nucleo;

namespace PhysicXNA.Niveles.Nivel2
{
    /// <summary>Escenario del Nivel 2.</summary>
    public class Nivel2 : Nivel
    {
        SoundEffect bien;
        SoundEffect mal;

        DuendeEstatico fondoMostrar;
        Hud2 hud;
        DuendeEstatico cuerdita;
        internal DuendeEstatico cursor;
        internal DuendeEstatico[] enemigos;
        DuendeEstatico[] cuerditasEnemigos;
        internal int numEnem;
        Random genNumAleatorios;
        Vector2 vectorResultante;

        internal TimeSpan tiempoMalo;
        internal TimeSpan anotacion;
        int vecesRepetido;
        int vecesCompletar;
        int tolerancia;

        /// <summary>
        /// Construye una instancia para el nivel 2.
        /// </summary>
        /// <param name="juego">Juego en que será mostrado el nivel.</param>
        /// <param name="rutaVideo">Ruta del archivo de video de introducción al nivel.</param>
        /// <param name="rutaMusica">Ruta del archivo de música que será reproducido en el nivel.</param>
        /// <param name="cuadrosPorSegundo"><seealso cref="Nivel.cuadrosPorSegundo"/></param>
        public Nivel2(Game juego, String rutaMusica, String rutaVideo, int cuadrosPorSegundo)
            : base(juego, rutaMusica, rutaVideo, cuadrosPorSegundo)
        {
            reglas[0] =
                "Apunta con el ratón en el plano cartesiano para intentar\n" +
                "descubir el punto que contrarrestará toda la fuerza de los\n" +
                "enemigos.\n" +
                "Ten cuidado de no exceder los límites porque serás penalizado\n" +
                "con tiempo.";
            reglas[1] =
                "Click principal - Seleccionar el punto de la fuerza opuesta";
            reglas[2] =
                "                                                 /* Vectores *\\\n\n" +
                "Los vectores, tanto en física, como en matemáticas, definen\n" +
                "la magnitud y dirección de una unidad de medida, como la fuerza\n" +
                "la potencia, la velocidad y cualquier tipo de energía.";

            fondoMostrar = new DuendeEstatico("Fondos/fondo2", Vector2.Zero);
            hud = new Hud2();
            cuerdita = new DuendeEstatico("Recursos/Nivel2/cuerditaCasiChida", Vector2.Zero);
            cursor = new DuendeEstatico("Recursos/Nivel2/tomandoCuerda",Vector2.Zero, Color.White, new Vector2(0.18f));

            enemigos = new DuendeEstatico[5];
            cuerditasEnemigos = new DuendeEstatico[5];
            for (int c = 0; c < enemigos.Length; c++)
            {
                enemigos[c] = new DuendeEstatico("Recursos/Nivel2/enemigo", Vector2.Zero, Color.White, new Vector2(0.18f));
                cuerditasEnemigos[c] = new DuendeEstatico("Recursos/Nivel2/cuerditaCasiChida", Vector2.Zero, new Color(64, 128, 64));
            }
            genNumAleatorios = new Random();
        }

        /// <summary><seealso cref="Nivel.LoadContent"/></summary>
        protected override void LoadContent()
        {
            bien = Content.Load<SoundEffect>("Sonidos/bien");
            mal = Content.Load<SoundEffect>("Sonidos/mal");
            fondoMostrar.CargarContenido(Content);
            cuerdita.CargarContenido(Content);
            cursor.CargarContenido(Content);
            hud.CargarContenido(Content);
            foreach (DuendeEstatico enemigo in enemigos) enemigo.CargarContenido(Content);
            foreach (DuendeEstatico cuerditaEnemigo in cuerditasEnemigos) cuerditaEnemigo.CargarContenido(Content);
            base.LoadContent();
        }

        /// <summary><seealso cref="Nivel.UnloadContent"/></summary>
        protected override void UnloadContent()
        {
            bien.Dispose();
            mal.Dispose();
            fondoMostrar.LiberarContenido();
            cuerdita.LiberarContenido();
            cursor.LiberarContenido();
            hud.LiberarContenido();
            foreach (DuendeEstatico enemigo in enemigos) enemigo.LiberarContenido();
            foreach (DuendeEstatico cuerditaEnemigo in cuerditasEnemigos) cuerditaEnemigo.LiberarContenido();
            base.UnloadContent();
        }

        private void Reiniciar()
        {
            vectorResultante = Vector2.Zero;
            for (int c = 0; c < numEnem; c++)
            {
                enemigos[c].OrigenGiro = new Vector2((float)enemigos[c].AnchoReal / 2, (float)enemigos[c].AltoReal);
                enemigos[c].Posicion = new Vector2((float)genNumAleatorios.NextDouble() * (resolucionPantalla.Width / numEnem - enemigos[c].Ancho) + resolucionPantalla.Width * (((float)numEnem - 1) / (2 * (float)numEnem)) + enemigos[c].Ancho / 2,
                    (float)genNumAleatorios.NextDouble() * (resolucionPantalla.Height / (2 * numEnem) - enemigos[c].Alto) + resolucionPantalla.Height * (((float)numEnem - 1) / (2 * (float)numEnem)) + enemigos[c].Alto);
                cuerditasEnemigos[c].AnguloGiro = (float)Math.Atan((enemigos[c].Posicion.Y - cuerditasEnemigos[c].Posicion.Y) / (enemigos[c].Posicion.X - cuerditasEnemigos[c].Posicion.X));
                if (enemigos[c].Posicion.X < cuerditasEnemigos[c].Posicion.X) cuerditasEnemigos[c].AnguloGiro += (float)Math.PI;
                enemigos[c].AnguloGiro = cuerditasEnemigos[c].AnguloGiro + (float)Math.PI / 2;
                cuerditasEnemigos[c].Escala = new Vector2((float)Math.Sqrt((enemigos[c].Posicion.X - cuerditasEnemigos[c].Posicion.X) * (enemigos[c].Posicion.X - cuerditasEnemigos[c].Posicion.X) + (enemigos[c].Posicion.Y - cuerditasEnemigos[c].Posicion.Y) * (enemigos[c].Posicion.Y - cuerditasEnemigos[c].Posicion.Y)) / cuerditasEnemigos[c].AnchoReal, 0.05f);
                vectorResultante -= new Vector2(enemigos[c].Posicion.X - resolucionPantalla.Width / 2, enemigos[c].Posicion.Y - resolucionPantalla.Height / 2);
            }
        }

        /// <summary><seealso cref="Nivel.Inicializar"/></summary>
        public override void Inicializar()
        {
            base.Inicializar();
            dibujarAntes.Add(fondoMostrar);
            dibujarAntes.Add(hud.BarraSuperior);

            hud.Inicializar(this);
            tolerancia = (opciones.dificultad == SistemaPerfiles.DificultadJuego.Difícil ? 30 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Medio ? 40 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Fácil ? 50 : 1) * resolucionPantalla.Width / 800;
            vecesRepetido = 0;
            vecesCompletar = opciones.dificultad == SistemaPerfiles.DificultadJuego.Fácil ? 5 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Medio ? 6 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Difícil ? 7 : 10;
            fondoMostrar.Escala = new Vector2((float)resolucionPantalla.Width / fondoMostrar.AnchoReal, (float)resolucionPantalla.Height / fondoMostrar.AltoReal);
            cuerdita.OrigenGiro = new Vector2(0, cuerdita.AltoReal / 2);
            cuerdita.Posicion = new Vector2(resolucionPantalla.Width / 2, resolucionPantalla.Height / 2);
            foreach (DuendeEstatico cuerditaEnemigo in cuerditasEnemigos)
            {
                cuerditaEnemigo.Posicion = cuerdita.Posicion;
                cuerditaEnemigo.OrigenGiro = cuerdita.OrigenGiro;
            }
            cursor.OrigenGiro = new Vector2((float)cursor.AnchoReal / 2, (float)cursor.AltoReal);
            numEnem = (opciones.dificultad == SistemaPerfiles.DificultadJuego.Fácil ? 2 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Medio ? 3 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Difícil ? 4 : 5);
            Reiniciar();
        }

        /// <summary><seealso cref="Nivel.Update"/></summary>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (reproduciendoVideo) return;
            cursor.Posicion = new Vector2(estadoRaton.X, estadoRaton.Y);

            for (int c = 0; c < numEnem; c++) enemigos[c].Actualizar(gameTime, resolucionPantalla, estadoTeclado);

            cuerdita.AnguloGiro = (float)Math.Atan((estadoRaton.Y - cuerdita.Posicion.Y) / (estadoRaton.X - cuerdita.Posicion.X));
            if (estadoRaton.X < cuerdita.Posicion.X) cuerdita.AnguloGiro += (float)Math.PI;
            cuerdita.Escala = new Vector2((float)Math.Sqrt((estadoRaton.X - cuerdita.Posicion.X) * (estadoRaton.X - cuerdita.Posicion.X) + (estadoRaton.Y - cuerdita.Posicion.Y) * (estadoRaton.Y - cuerdita.Posicion.Y)) / cuerdita.AnchoReal, 0.05f);
            cursor.AnguloGiro = (float)Math.Atan((estadoRaton.Y - cuerdita.Posicion.Y) / (estadoRaton.X - cuerdita.Posicion.X));
            if (estadoRaton.X < cuerdita.Posicion.X) cursor.AnguloGiro += (float)Math.PI;
            cursor.AnguloGiro += (float)Math.PI / 2;

            if(opciones.invertir_mouse == 0 ? (estadoRaton.LeftButton == ButtonState.Released && estadoRatonAnt.LeftButton == ButtonState.Pressed) :
                (estadoRaton.RightButton == ButtonState.Released && estadoRatonAnt.RightButton == ButtonState.Pressed))
            {
                Vector2 vectorRespuesta = new Vector2(cursor.Posicion.X - resolucionPantalla.Width / 2, cursor.Posicion.Y - resolucionPantalla.Height / 2);
                if (vectorRespuesta.X < vectorResultante.X + tolerancia && vectorRespuesta.X > vectorResultante.X - tolerancia &&
                    vectorRespuesta.Y < vectorResultante.Y + tolerancia && vectorRespuesta.Y > vectorResultante.Y - tolerancia)
                {
                    vecesRepetido++;
                    anotacion = new TimeSpan(0, 0, 3);
                    hud.Anotaciones.Texto = vecesRepetido.ToString();
                    hud.Anotaciones.Color = Color.Green;
                    if (vecesRepetido >= vecesCompletar) nivelCompletado = true;
                    Reiniciar();
                    if (opciones.sonidos != 0) bien.Play();
                }
                else
                {
                    tiempoCompletado += new TimeSpan(0, 0, opciones.dificultad == SistemaPerfiles.DificultadJuego.Fácil ? 10 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Medio ? 20 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Difícil ? 30 : 0);
                    tiempoMalo = new TimeSpan(0, 0, 3);
                    hud.Tiempo.Color = Color.Red;
                    if (opciones.sonidos != 0) mal.Play();
                }
            }

            hud.Actualizar(gameTime);
            estadoTecladoAnt = estadoTeclado;
            estadoRatonAnt = estadoRaton;
        }

        /// <summary><seealso cref="Nivel.Draw"/></summary>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (reproduciendoVideo) return;
            hud.Dibujar(spriteBatch, gameTime);
            for (int c = 0; c < numEnem; c++)
            {
                cuerditasEnemigos[c].Dibujar(spriteBatch, gameTime);
                enemigos[c].Dibujar(spriteBatch, gameTime);
            }
            cuerdita.Dibujar(spriteBatch, gameTime);
            cursor.Dibujar(spriteBatch, gameTime);
            spriteBatch.End();
        }
    }
}
