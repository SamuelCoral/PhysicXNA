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

namespace PhysicXNA.Niveles.Nivel1
{
    /// <summary>Escenario del Nivel 1.</summary>
    public class Nivel1 : Nivel
    {
        SoundEffect bien;
        SoundEffect mal;

        DuendeEstatico fondoMostrar;
        internal DuendeAnimado[] plataformas;
        
        DuendeEstatico pedestal;
        DuendeEstatico cursor;

        //hud
        Texto3D tiempo;
        Texto3D mensaje;
        Texto3D anotaciones;

        // Herramientas
        TimeSpan tiempoMalo;
        TimeSpan anotacion;
        Rectangle areaPedestal;
        Rectangle[] posicionHerramientas = new Rectangle[3];
        DuendeEstatico[] herramientas = new DuendeEstatico[10];
        String[] queMide;
        int[] herramientasEscogidas = new int[3];
        List<int> herramientasAnteriores = new List<int>();
        int herramientaAgarrar;
        PersonajeOriginal monito;
        PersonajeOriginal ia;
        Texto3D objetivo;
        bool herramanientaCorrecta;
        int vecesRepetido;
        int vecesCompletar;
        
        Random generadorNumAleat;

        /// <summary>
        /// Construye una instancia para el nivel 1.
        /// </summary>
        /// <param name="juego">Juego en que será mostrado el nivel.</param>
        /// <param name="rutaMusica">Ruta del archivo de música que será reproducido en el nivel.</param>
        /// <param name="rutaVideo">Ruta del archivo de video de introducción al nivel.</param>
        /// <param name="cuadrosPorSegundo"><seealso cref="Nivel.cuadrosPorSegundo"/></param>
        public Nivel1(Game juego, String rutaMusica, String rutaVideo, int cuadrosPorSegundo)
            : base(juego, rutaMusica, rutaVideo, cuadrosPorSegundo)
        {
            reglas[0] =
                "Recoje el objeto que se indica en la parte superior\n" +
                "de la pantalla saltando de plataforma en plataforma hasta\n" +
                "llegar al objeto indicado.\n" +
                "Cuidado con tomar un objeto equivocado, si lo haces\n"+
                "serás penalizado con tiempo de juego.";
            reglas[1] =
                "Izquierda o A                           - Desplazarse hacía la izquierda\n" +
                "Derecha o D                             - Desplazarse hacía la derecha\n" +
                "Arriba o W                               - Saltar\n" +
                "Abajo o S                                 - Bajar de plataforma\n" +
                "Espacio o Click principal   - Tomar la herramienta\n";
            reglas[2] =
                "                                              /*  Unidades de medición  *\\\n\n" +
                "La física es la ciencia que estudia el comportamiento del universo\n" +
                "y todo lo que lo conforma, para poder entender dichos fenómenos,\n" +
                "necesitamos cuantificarlos en ciertas unidades y existen herramientas\n" +
                "para percibir cada tipo de energía.";

            generadorNumAleat = new Random();
            fondoMostrar = new DuendeEstatico("Fondos/fondo1", Vector2.Zero);
            plataformas = new DuendeAnimado[12];
            int c;
            for(c = 0; c < plataformas.Length; c++)
            {
                plataformas[c] = new DuendeAnimado("Recursos/Nivel1/Spriteplataforma", Vector2.Zero, new Point(427, 134), new Point(2, 1), 3);
            }
            pedestal = new DuendeEstatico("Recursos/Nivel1/pedestal", Vector2.Zero,Color.Blue);
            herramientas[0] = new DuendeEstatico("Recursos/Nivel1/cantidadsus", Vector2.Zero);
            herramientas[1] = new DuendeEstatico("Recursos/Nivel1/intensidadlum", Vector2.Zero);
            herramientas[2] = new DuendeEstatico("Recursos/Nivel1/longitud", Vector2.Zero);
            herramientas[3] = new DuendeEstatico("Recursos/Nivel1/masa", Vector2.Zero);
            herramientas[4] = new DuendeEstatico("Recursos/Nivel1/tiempo", Vector2.Zero);
            herramientas[5] = new DuendeEstatico("Recursos/Nivel1/temperatura", Vector2.Zero);
            herramientas[6] = new DuendeEstatico("Recursos/Nivel1/corriente", Vector2.Zero);
            herramientas[7] = new DuendeEstatico("Recursos/Nivel1/fuerza", Vector2.Zero);
            herramientas[8] = new DuendeEstatico("Recursos/Nivel1/presion", Vector2.Zero);
            herramientas[9] = new DuendeEstatico("Recursos/Nivel1/potencia", Vector2.Zero);
            queMide = new String[herramientas.Length];
            queMide[0] = "Cantidad de Sustancia";
            queMide[1] = "Intensidad Luminosa";
            queMide[2] = "Longitud";
            queMide[3] = "Masa";
            queMide[4] = "Tiempo";
            queMide[5] = "Temperatura";
            queMide[6] = "Corriente";
            queMide[7] = "Fuerza";
            queMide[8] = "Presion";
            queMide[9] = "Potencia";
            

            monito = new PersonajeOriginal(Vector2.Zero, 15);
            ia = new PersonajeOriginal(Vector2.Zero, 20, IA: true);
            tiempo = new Texto3D("Fuentes/TextoTiempo", "", new Vector2(10, 10), 30, new Color(172,228,255,255));
            mensaje = new Texto3D("Fuentes/TextoMensaje", "", new Vector2(10, 50), 30, new Color(172, 228, 255, 255));
            anotaciones = new Texto3D("Fuentes/TextoAnotaciones", String.Empty, Vector2.Zero, 30, Color.Red, 3);
            objetivo = new Texto3D("Fuentes/TextoMensaje", "", Vector2.Zero, 30, Color.Yellow);
            cursor = new DuendeEstatico("Cursores/Cursor", Vector2.Zero, Color.White, new Vector2(0.1f));
        }

        /// <summary><seealso cref="Nivel.LoadContent"/></summary>
        protected override void LoadContent()
        {
            bien = Content.Load<SoundEffect>("Sonidos/bien");
            mal = Content.Load<SoundEffect>("Sonidos/mal");
            fondoMostrar.CargarContenido(Content);
            foreach (DuendeAnimado plataforma in plataformas) plataforma.CargarContenido(Content);
           
            pedestal.CargarContenido(Content);
           
            foreach (DuendeEstatico herramienta in herramientas) herramienta.CargarContenido(Content);
            monito.CargarContenido(Content);
            ia.CargarContenido(Content);
            tiempo.CargarContenido(Content);
            mensaje.CargarContenido(Content);
            anotaciones.CargarContenido(Content);
            objetivo.CargarContenido(Content);
            cursor.CargarContenido(Content);
            base.LoadContent();
        }

        /// <summary><seealso cref="Nivel.UnloadContent"/></summary>
        protected override void UnloadContent()
        {
            bien.Dispose();
            mal.Dispose();
            fondoMostrar.LiberarContenido();
            foreach (DuendeAnimado plataforma in plataformas) plataforma.LiberarContenido();
           
            pedestal.LiberarContenido();
           
            foreach (DuendeEstatico herramienta in herramientas) herramienta.LiberarContenido();
            monito.LiberarContenido();
            ia.LiberarContenido();
            tiempo.LiberarContenido();
            mensaje.LiberarContenido();
            anotaciones.LiberarContenido();
            objetivo.LiberarContenido();
            cursor.LiberarContenido();
            base.UnloadContent();
        }

        private void Reiniciar()
        {
            tiempoMalo = TimeSpan.Zero;
            foreach (DuendeAnimado plataforma in plataformas) plataforma.Escala = new Vector2(0.25f, 0.25f);
            int x, y;
            for (y = 0; y < 4; y++)
            {
                for (x = 0; x < 3; x++)
                {
                    plataformas[y * 3 + x].Posicion = new Vector2((float)generadorNumAleat.NextDouble() * (resolucionPantalla.Width / 3 - plataformas[0].Ancho) + x * resolucionPantalla.Width / 3, (float)generadorNumAleat.NextDouble() * 40 - 20 + y * (resolucionPantalla.Height - 200) / 4 + (resolucionPantalla.Height - 200) / 8 + 125);
                }
            }

            int c, cr;
            bool volverABuscar;
			bool ahogado = true;

            for (c = 0; c < 3; c++)
            {
                do
                {
                    volverABuscar = false;
                    herramientasEscogidas[c] = generadorNumAleat.Next(herramientas.Length);
                    for (cr = c - 1; cr >= 0; cr--) if (herramientasEscogidas[c] == herramientasEscogidas[cr]) volverABuscar = true;
                    foreach (int herramienta in herramientasAnteriores) if (herramienta == herramientasEscogidas[c]) { Console.Write("Volver a buscar "); volverABuscar = true; }
                    if (herramientasEscogidas[c] < herramientas.Length - 3 && !volverABuscar) { Console.Write("Desahogado "); ahogado = false; }
                    
                } while (volverABuscar || (ahogado && c == 2));

                herramientas[herramientasEscogidas[c]].Escala = new Vector2(75f / herramientas[herramientasEscogidas[c]].AnchoReal, 75f / herramientas[herramientasEscogidas[c]].AltoReal);
                herramientas[herramientasEscogidas[c]].Posicion = new Vector2(plataformas[c].Posicion.X + (plataformas[c].Ancho - herramientas[herramientasEscogidas[c]].Ancho) / 2, plataformas[c].Posicion.Y - herramientas[herramientasEscogidas[c]].Alto);
                posicionHerramientas[c] = new Rectangle((int)herramientas[herramientasEscogidas[c]].Posicion.X, (int)herramientas[herramientasEscogidas[c]].Posicion.Y, herramientas[herramientasEscogidas[c]].Ancho, herramientas[herramientasEscogidas[c]].Alto);
            }

            ia.Posicion = new Vector2((resolucionPantalla.Width - ia.Ancho) / 2, resolucionPantalla.Height - ia.Alto);
            pedestal.Color = Color.Crimson;
            do herramientaAgarrar = generadorNumAleat.Next(herramientasEscogidas.Length); while (herramientasEscogidas[herramientaAgarrar] >= herramientas.Length - 3);
            objetivo.Texto = queMide[herramientasEscogidas[herramientaAgarrar]];
            herramanientaCorrecta = false;
        }

        /// <summary><seealso cref="Nivel.Inicializar"/></summary>
        public override void Inicializar()
        {
            base.Inicializar();
            dibujarAntes.Add(fondoMostrar);

            herramientasAnteriores.Clear();
            anotacion = TimeSpan.Zero;
            monito.Inicializar();
            ia.Inicializar();
            ia.Velocidad = opciones.dificultad == SistemaPerfiles.DificultadJuego.Fácil ? 2f : opciones.dificultad == SistemaPerfiles.DificultadJuego.Medio ? 2.3f : opciones.dificultad == SistemaPerfiles.DificultadJuego.Difícil ? 2.6f : 1e+20f;
            vecesRepetido = 0;
            vecesCompletar = opciones.dificultad == SistemaPerfiles.DificultadJuego.Fácil ? 5 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Medio ? 6 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Difícil ? 7 : 1000;
            fondoMostrar.Escala = new Vector2((float)resolucionPantalla.Width / fondoMostrar.AnchoReal, (float)resolucionPantalla.Height / fondoMostrar.AltoReal);
            monito.Posicion = new Vector2(50, resolucionPantalla.Height - monito.Alto);
            Reiniciar();
            pedestal.Escala = new Vector2(0.25f, 0.25f);
            pedestal.Posicion = new Vector2(resolucionPantalla.Width - pedestal.Ancho - 25, resolucionPantalla.Height - pedestal.Alto);
            areaPedestal = new Rectangle((int)pedestal.Posicion.X, (int)pedestal.Posicion.Y, pedestal.Ancho, pedestal.Alto);
            anotaciones.Texto = "0";
            anotaciones.Posicion = new Vector2(botonAyuda.Posicion.X - 30 - anotaciones.TamTexto.X, 10);
        }

        /// <summary><seealso cref="Nivel.Update"/></summary>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (reproduciendoVideo) return;

            cursor.Posicion = new Vector2(estadoRaton.X, estadoRaton.Y);
            tiempo.Texto = tiempoCompletado.Minutes.ToString("00") + ":" + tiempoCompletado.Seconds.ToString("00");
            objetivo.Actualizar(gameTime);
            monito.Actualizar(gameTime, resolucionPantalla, estadoTeclado, opciones, this);
            ia.Actualizar(gameTime, resolucionPantalla, estadoTeclado, opciones, this, monito.Posicion);
            
            Rectangle areaMonito = new Rectangle((int)monito.Posicion.X,(int)monito.Posicion.Y,monito.Ancho,monito.Alto);
            if (((opciones.invertir_mouse != 0 ? estadoRaton.RightButton == ButtonState.Pressed && estadoRatonAnt.RightButton == ButtonState.Released :
                estadoRaton.LeftButton == ButtonState.Pressed && estadoRatonAnt.LeftButton == ButtonState.Released) ||
                (estadoTeclado.IsKeyDown(Keys.Space) && !estadoTecladoAnt.IsKeyDown(Keys.Space))) && !herramanientaCorrecta) 
            {
                for (int i = 0; i < 3; i++)
                {
                    if (posicionHerramientas[i].Intersects(areaMonito))
                    {
                        if (i == herramientaAgarrar)
                        {
                            herramanientaCorrecta = true;
                            pedestal.Color = Color.White;
                            if (opciones.sonidos != 0) bien.Play();
                        }
                        else {

                            tiempoCompletado += new TimeSpan(0, 0, opciones.dificultad == SistemaPerfiles.DificultadJuego.Fácil ? 30 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Medio ? 45 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Difícil ? 60 : 0);
                            tiempoMalo = new TimeSpan(0, 0, 3);
                            tiempo.Color = Color.Red;
                            if (opciones.sonidos != 0) mal.Play();
                        }
                    }
                }
            }
            if (herramanientaCorrecta && areaPedestal.Intersects(areaMonito))
            {
                vecesRepetido++;
                anotaciones.Texto = vecesRepetido.ToString();
                anotacion = new TimeSpan(0, 0, 3);
                anotaciones.Color = Color.Navy;

                if (opciones.sonidos != 0) bien.Play();
                if(vecesRepetido >= vecesCompletar) nivelCompletado = true;
                herramientasAnteriores.Add(herramientasEscogidas[herramientaAgarrar]);
                if(!nivelCompletado) Reiniciar();
            }

            if (areaMonito.Intersects(new Rectangle((int)ia.Posicion.X, (int)ia.Posicion.Y, ia.Ancho, ia.Alto)))
            {
                tiempoCompletado += new TimeSpan(0, 0, opciones.dificultad == SistemaPerfiles.DificultadJuego.Fácil ? 5 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Medio ? 10 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Difícil ? 15 : 0);
                tiempoMalo = new TimeSpan(0, 0, 3);
                tiempo.Color = Color.Red;
                monito.Posicion = new Vector2(50, resolucionPantalla.Height - monito.Alto);
                ia.Posicion = new Vector2((resolucionPantalla.Width - ia.Ancho) / 2, resolucionPantalla.Height - ia.Alto);
                if (opciones.sonidos != 0) mal.Play();
            }

            foreach (DuendeAnimado plataforma in plataformas) plataforma.Actualizar(gameTime, resolucionPantalla, estadoTeclado);

            if (tiempoMalo >= TimeSpan.Zero) { tiempo.Actualizar(gameTime); tiempoMalo -= gameTime.ElapsedGameTime; }
            else { tiempo.Color = Color.Yellow; tiempo.Reiniciar(); }
            if (anotacion >= TimeSpan.Zero) { anotaciones.Actualizar(gameTime); anotacion -= gameTime.ElapsedGameTime; }
            else { anotaciones.Color = Color.Red; anotaciones.Reiniciar(); }

            if (herramanientaCorrecta) mensaje.Texto = "Coloca el objeto sobre el pedestal";
            else mensaje.Texto = "Recoje el objeto relacionado a: ";
            objetivo.Posicion = new Vector2(mensaje.Posicion.X + mensaje.TamTexto.X, mensaje.Posicion.Y);

            estadoTecladoAnt = estadoTeclado;
            estadoRatonAnt = estadoRaton;
        }

        /// <summary><seealso cref="Nivel.Draw"/></summary>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (reproduciendoVideo) return;
            pedestal.Dibujar(spriteBatch, gameTime);
            foreach (DuendeAnimado plataforma in plataformas) plataforma.Dibujar(spriteBatch, gameTime);
            for (int herramientaADibujar = 0; herramientaADibujar < herramientasEscogidas.Length; herramientaADibujar++)
                if (!herramanientaCorrecta || herramientaADibujar != herramientaAgarrar)
                    herramientas[herramientasEscogidas[herramientaADibujar]].Dibujar(spriteBatch, gameTime);
            ia.Dibujar(spriteBatch, gameTime);
            monito.Dibujar(spriteBatch, gameTime);
            tiempo.Dibujar(spriteBatch, gameTime);
            mensaje.Dibujar(spriteBatch, gameTime);
            if (!herramanientaCorrecta) objetivo.Dibujar(spriteBatch, gameTime);
            anotaciones.Dibujar(spriteBatch, gameTime);
            cursor.Dibujar(spriteBatch, gameTime);
            spriteBatch.End();
        }
    }
}
