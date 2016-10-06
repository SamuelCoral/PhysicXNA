using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using PhysicXNA.Nucleo;

namespace PhysicXNA.Niveles.Nivel4
{
    /// <summary>
    /// Indica el estado de un objeto para el nivel 4.
    /// </summary>
    public enum EstadoObjeto
    {
        /// <summary>Obeto inmóvil en el piso.</summary>
        EnReposo,
        /// <summary>Animación de empuje.</summary>
        Empujando,
        /// <summary>Objeto deslizándose en el piso.</summary>
        Deslizandose,
        /// <summary>Objeto cayendo al vacío.</summary>
        Cayendo,
        /// <summary>Objeto que ha caido totalmente al vacío.</summary>
        Caido
    }

    /// <summary>Escenario del Nivel 4.</summary>
    public class Nivel4 : Nivel
    {
        DuendeEstatico fondo;
        DuendeEstatico piso;
        DuendeAnimado monitoEmpujando;
        DuendeAnimado monitoDeslizandose1;
        DuendeAnimado monitoDeslizandose2;
        DuendeEstatico monitoCayendo;
        DuendeEstatico monitoParado;
        DuendeEstatico caja;
        DuendeEstatico hoyito;
        Texto3D distancia;
        DuendeEstatico lineaDistancia;

        float friccion = 0.005f;
        float gravedad = 9.8f;
        int pixelesXMetro = 100;
        int KGXM3 = 50;
        float masaCaja;
        float masaMono;
        

        //hud
        Texto3D tiempo;
        Texto3D mensaje;
        Texto3D anotaciones;
        DuendeEstatico cursor;
        TimeSpan tiempoMalo;
        TimeSpan anotacion;
        SoundEffect mal;
        SoundEffect bien;
        SoundEffect empuje;
        // Herramientas
        DuendeEstatico barraVacia;
        DuendeEstatico barraLlena;
        float tiempoRebote = 2;
        bool barritaAvanzando = true;
        int vecesCompletado;
        int vecesRepetir;
        Random genNumRnd;


        /// <summary>
        /// Construye una instancia para el nivel 4.
        /// </summary>
        /// <param name="juego">Juego en que será mostrado el nivel.</param>
        /// <param name="rutaVideo">Ruta del archivo de video de introducción al nivel.</param>
        /// <param name="rutaMusica">Ruta del archivo de música que será reproducido en el nivel.</param>
        /// <param name="cuadrosPorSegundo"><seealso cref="Nivel.cuadrosPorSegundo"/></param>
        public Nivel4(Game juego, String rutaMusica, String rutaVideo, int cuadrosPorSegundo)
            : base(juego, rutaMusica, rutaVideo, cuadrosPorSegundo)
        {
            reglas[0] =
                "Empuja la caja con la fuerza suficiente para hacerla\n" +
                "caer de la plataforma.\n" +
                "Cuidado que el piso resbaloso hará que tu fuerza\n" +
                "también te empuje, intenta encontrar una fuerza suficiente\n" +
                "para tirar la caja sin tirarte a tí también.";
            reglas[1] =
                "Click pulsado - Cargar o descargar fuerza\n" +
                "Click soltado - Usar la fuerza cargada para empujar la caja";
            reglas[2] =
                "                                             /* Leyes de Newton *\\\n\n" +
                "Las 3 leyes de Newton nos hablán acerca del movimiento y son los\n" +
                "cimientos de la mecánica clásica y son:\n" +
                " - Ley de la inercia, la relación entre fuerza, aceleración y masa y el\n" +
                "principio de acción y reacción.";

            genNumRnd = new Random();
            fondo = new DuendeEstatico("Fondos/fondo3", Vector2.Zero);
            tiempo = new Texto3D("Fuentes/TextoTiempo", "", new Vector2(10, 10), 30);
            mensaje = new Texto3D("Fuentes/TextoMensaje", "", new Vector2(10, 50), 30, new Color(172, 228, 255, 255));
            anotaciones = new Texto3D("Fuentes/TextoAnotaciones", String.Empty, Vector2.Zero, 30, Color.Red, 3);
            cursor = new DuendeEstatico("Cursores/Cursor", Vector2.Zero, Color.White, new Vector2(0.1f));

            barraVacia = new DuendeEstatico("Recursos/Nivel4/barrita", Vector2.Zero, Color.Black);
            barraLlena = new DuendeEstatico("Recursos/Nivel4/barrita", Vector2.Zero);

            monitoParado = new DuendeEstatico("Recursos/parado", Vector2.Zero);
            monitoCayendo = new DuendeEstatico("Recursos/brincando", Vector2.Zero);
            monitoEmpujando = new DuendeAnimado("Recursos/Nivel4/EmpujeSprite", Vector2.Zero, new Point(194, 281), new Point(7, 1), 24, Color.White, new Vector2(1f));
            monitoDeslizandose1 = new DuendeAnimado("Recursos/Nivel4/DPESprite", Vector2.Zero, new Point(195, 292), new Point(5, 1), 12, Color.White, new Vector2(1f));
            monitoDeslizandose2 = new DuendeAnimado("Recursos/Nivel4/DDESprite", Vector2.Zero, new Point(195, 293), new Point(4, 1), 12, Color.White, new Vector2(1f));
            
            piso = new DuendeEstatico("Recursos/Nivel4/piso", Vector2.Zero);
            caja = new DuendeEstatico("Recursos/Nivel4/crate", Vector2.Zero);
            hoyito = new DuendeEstatico("Recursos/Nivel4/hoyitoxD", Vector2.Zero);
            lineaDistancia = new DuendeEstatico("Recursos/Nivel4/lineaxD", Vector2.Zero);

            distancia = new Texto3D("Fuentes/TextoMensaje", "", new Vector2(100, 10), 30, Color.White);
        }

        /// <summary><seealso cref="Nivel.LoadContent"/></summary>
        protected override void LoadContent()
        {
            bien = Content.Load<SoundEffect>("Sonidos/bien");
            mal = Content.Load<SoundEffect>("Sonidos/mal");
            empuje = Content.Load<SoundEffect>("Sonidos/empuje");
            fondo.CargarContenido(Content);
            monitoParado.CargarContenido(Content);
            monitoCayendo.CargarContenido(Content);
            monitoEmpujando.CargarContenido(Content);
            monitoDeslizandose1.CargarContenido(Content);
            monitoDeslizandose2.CargarContenido(Content);
            tiempo.CargarContenido(Content);
            mensaje.CargarContenido(Content);
            anotaciones.CargarContenido(Content);
            cursor.CargarContenido(Content);
            barraVacia.CargarContenido(Content);
            barraLlena.CargarContenido(Content);
            piso.CargarContenido(Content);
            caja.CargarContenido(Content);
            hoyito.CargarContenido(Content);
            distancia.CargarContenido(Content);
            lineaDistancia.CargarContenido(Content);
            base.LoadContent();
        }

        /// <summary><seealso cref="Nivel.UnloadContent"/></summary>
        protected override void UnloadContent()
        {
            bien.Dispose();
            mal.Dispose();
            empuje.Dispose();
            fondo.LiberarContenido();
            monitoParado.LiberarContenido();
            monitoCayendo.LiberarContenido();
            monitoEmpujando.LiberarContenido();
            monitoDeslizandose1.LiberarContenido();
            monitoDeslizandose2.LiberarContenido();
            tiempo.LiberarContenido();
            mensaje.LiberarContenido();
            anotaciones.LiberarContenido();
            cursor.LiberarContenido();
            barraLlena.LiberarContenido();
            barraVacia.LiberarContenido();
            piso.LiberarContenido();
            caja.LiberarContenido();
            hoyito.LiberarContenido();
            distancia.LiberarContenido();
            lineaDistancia.LiberarContenido();
            base.UnloadContent();
        }
        int distanciaC;

        private void Reiniciar()
        {
            tiempoMalo = TimeSpan.Zero;
            estadoCaja = estadoMonito = EstadoObjeto.EnReposo;
            velocidadCayendo = velocidadMonitoCayendo = 0;
            caja.Escala = new Vector2((float)genNumRnd.NextDouble() * 0.125f + 0.15f);
            //caja.Escala = new Vector2(0.35f);
            masaCaja = (float)Math.Pow((float)caja.Ancho / pixelesXMetro, 3f) * KGXM3;
            //masaCaja = masaMono;
            monitoParado.Posicion = monitoCayendo.Posicion = monitoEmpujando.Posicion =
                new Vector2(piso.Ancho * ((caja.Escala.X - 0.15f) / 0.2f * 2 / 5f) + (piso.Ancho * 2 / 5f) + piso.Posicion.X, piso.Posicion.Y - monitoParado.Alto);
                //new Vector2((float)piso.Posicion.X, (float)piso.Posicion.Y - monitoParado.Alto);
            caja.Posicion = new Vector2(monitoParado.Posicion.X + monitoParado.Ancho, monitoParado.Posicion.Y + monitoParado.Alto - caja.Alto);
        }
        
        /// <summary><seealso cref="Nivel.Inicializar"/></summary>
        public override void Inicializar()
        {
            base.Inicializar();
            dibujarAntes.Add(fondo);

            //monito.Inicializar();
            monitoCayendo.Escala = new Vector2((float)monitoParado.Ancho / monitoCayendo.AnchoReal, (float)monitoParado.Alto / monitoCayendo.AltoReal);
            monitoEmpujando.Escala = new Vector2((float)monitoParado.Ancho / monitoEmpujando.AnchoReal, (float)monitoParado.Alto / monitoEmpujando.AltoReal);
            monitoDeslizandose1.Escala = new Vector2((float)monitoParado.Ancho / monitoDeslizandose1.AnchoReal, (float)monitoParado.Alto / monitoDeslizandose1.AltoReal);
            monitoDeslizandose2.Escala = new Vector2((float)monitoParado.Ancho / monitoDeslizandose2.AnchoReal, (float)monitoParado.Alto / monitoDeslizandose2.AltoReal);

            barraLlena.Escala = barraVacia.Escala = new Vector2(((float)resolucionPantalla.Width - 300) / barraVacia.AnchoReal, 1f);
            barraVacia.Posicion = barraLlena.Posicion = new Vector2((resolucionPantalla.Width - barraLlena.Ancho) / 2, 100);
            piso.Escala = new Vector2(((float)resolucionPantalla.Width - 200) / piso.AnchoReal, 1f);
            piso.Posicion = new Vector2((resolucionPantalla.Width - piso.Ancho) / 2, resolucionPantalla.Height - piso.Alto - 100);
            fondo.Escala = new Vector2((float)resolucionPantalla.Width / fondo.AnchoReal, (float)resolucionPantalla.Height / fondo.AltoReal);
            distancia.Texto = "0";
            //distancia.Posicion = new Vector2(piso.Posicion.X + piso.Ancho - 100, piso.Posicion.Y + piso.Alto);
            distancia.Posicion = new Vector2(caja.Posicion.X + caja.Ancho, piso.Posicion.Y + piso.Alto + 5);

            hoyito.Posicion = new Vector2(0, (float)resolucionPantalla.Height - 100);
            hoyito.Escala = new Vector2((float)resolucionPantalla.Width / hoyito.AnchoReal, (float)(resolucionPantalla.Height - hoyito.Posicion.Y) / hoyito.AltoReal);
            lineaDistancia.Posicion = new Vector2(piso.Posicion.X + piso.Ancho, piso.Posicion.Y + piso.Alto);
            lineaDistancia.OrigenGiro = new Vector2(0, lineaDistancia.AltoReal);
            lineaDistancia.AnguloGiro = (float)Math.PI;
            masaMono = 80f;

            mensaje.Texto = "Empuja la caja fuera del piso sin caerte tu.";
            anotaciones.Texto = "0";
            anotaciones.Posicion = new Vector2(botonAyuda.Posicion.X - 30 - anotaciones.TamTexto.X, 10);
            anotacion = TimeSpan.Zero;
            vecesCompletado = 0;
            vecesRepetir = opciones.dificultad == SistemaPerfiles.DificultadJuego.Fácil ? 7 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Medio ? 10 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Difícil ? 15 : 1;

            Reiniciar();
        }

        float vi, velocidad, viMono, velocidadMono, velocidadCayendo, velocidadMonitoCayendo;
        long tiempoTranscurrido;
        int dx;
        EstadoObjeto estadoCaja, estadoMonito;

        /// <summary><seealso cref="Nivel.Update"/></summary>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (reproduciendoVideo) return;

            cursor.Posicion = new Vector2(estadoRaton.X, estadoRaton.Y);

            monitoDeslizandose1.Actualizar(gameTime, resolucionPantalla, estadoTeclado);
            monitoDeslizandose2.Actualizar(gameTime, resolucionPantalla, estadoTeclado);

            lineaDistancia.Escala = new Vector2((float)distanciaC / lineaDistancia.AnchoReal, 1f);
            distanciaC = ((int)piso.Posicion.X + piso.Ancho) - ((int)caja.Posicion.X + caja.Ancho);
            distancia.Texto = distanciaC.ToString() + "cm";
            distancia.Posicion = new Vector2(lineaDistancia.Posicion.X - lineaDistancia.Ancho - distancia.TamTexto.X - 5, lineaDistancia.Posicion.Y);
            tiempo.Texto = tiempoCompletado.Minutes.ToString("00") + ":" + tiempoCompletado.Seconds.ToString("00");

            if ((opciones.invertir_mouse == 0 ? estadoRaton.LeftButton == ButtonState.Pressed : estadoRaton.RightButton == ButtonState.Pressed)
                && estadoCaja == EstadoObjeto.EnReposo && estadoMonito == EstadoObjeto.EnReposo)
            {
                barraLlena.Escala = new Vector2(barraLlena.Escala.X + (float)gameTime.ElapsedGameTime.Milliseconds / 1000 * barraVacia.Ancho / barraLlena.AnchoReal / tiempoRebote * (barritaAvanzando ? 1 : -1), barraLlena.Escala.Y);
                if (barraLlena.Ancho > barraVacia.Ancho)
                {
                    barraLlena.Escala = new Vector2((float)barraVacia.Ancho / barraLlena.AnchoReal, barraLlena.Escala.Y);
                    barritaAvanzando = false;
                }
                if (barraLlena.Ancho <= 0)
                {
                    barraLlena.Escala = new Vector2(0, barraLlena.Escala.Y);
                    barritaAvanzando = true;
                }

                barraLlena.Color = new Color((float)barraLlena.Ancho / barraVacia.Ancho < 0.5f ? (float)barraLlena.Ancho / barraVacia.Ancho * 2 : 1f,
                    (float)barraLlena.Ancho / barraVacia.Ancho < 0.5f ? 1f : 1f - (((float)barraLlena.Ancho / barraVacia.Ancho) - 0.5f) * 2, 0);
            }

            if ((opciones.invertir_mouse == 0 ? estadoRaton.LeftButton == ButtonState.Released && estadoRatonAnt.LeftButton == ButtonState.Pressed : estadoRaton.RightButton == ButtonState.Released && estadoRatonAnt.RightButton == ButtonState.Pressed) &&
                estadoCaja == EstadoObjeto.EnReposo && estadoMonito == EstadoObjeto.EnReposo)
            {
                //vi = barraLlena.Escala.X * 10 - gravedad * masaCaja * friccion * 2;
                vi = (float)Math.Sqrt((piso.Ancho * (double)barraLlena.Ancho / barraVacia.Ancho) / pixelesXMetro * masaMono * 0.1f) -
                    gravedad * masaCaja * friccion * 2 / pixelesXMetro;
                viMono = (float)Math.Sqrt((piso.Ancho * (double)barraLlena.Ancho / barraVacia.Ancho) / pixelesXMetro * masaMono * 0.1f) -
                    gravedad * masaMono * friccion * 2 / pixelesXMetro;
                tiempoTranscurrido = 0;
                monitoEmpujando.CuadroActual = Point.Zero;
                estadoMonito = estadoCaja = EstadoObjeto.Empujando;
                if (opciones.sonidos != 0) empuje.Play();
            }
            if (estadoMonito != EstadoObjeto.EnReposo || estadoCaja != EstadoObjeto.EnReposo)
            {
                tiempoTranscurrido += gameTime.ElapsedGameTime.Milliseconds;

                if (estadoCaja == EstadoObjeto.Cayendo)
                {
                    velocidadCayendo += gravedad * gameTime.ElapsedGameTime.Milliseconds / 1000f;
                    dx = (int)(velocidadCayendo * (gameTime.ElapsedGameTime.Milliseconds / 1000f) * pixelesXMetro);
                    caja.Posicion = new Vector2(caja.Posicion.X + velocidad * pixelesXMetro * gameTime.ElapsedGameTime.Milliseconds / 1000f, caja.Posicion.Y + dx);
                    if (caja.Posicion.Y > hoyito.Posicion.Y) estadoCaja = EstadoObjeto.Caido;
                }
                if (estadoCaja == EstadoObjeto.Deslizandose)
                {
                    velocidad = vi - friccion * gravedad * masaCaja * (tiempoTranscurrido / 1000f);
                    dx = (int)(velocidad * (gameTime.ElapsedGameTime.Milliseconds / 1000f) * pixelesXMetro);
                    caja.Posicion = new Vector2(caja.Posicion.X + dx, caja.Posicion.Y);
                    if (velocidad <= 0) estadoCaja = EstadoObjeto.EnReposo;
                    if (caja.Posicion.X - piso.Posicion.X > piso.Ancho) estadoCaja = EstadoObjeto.Cayendo;
                }
                if (estadoMonito == EstadoObjeto.Empujando)
                {
                    if (monitoEmpujando.CuadroActual.X == monitoEmpujando.NumCuadros.X - 1 && monitoEmpujando.CuadroActual.Y == monitoEmpujando.NumCuadros.Y - 1)
                        estadoMonito = estadoCaja = EstadoObjeto.Deslizandose;
                    monitoEmpujando.Actualizar(gameTime, resolucionPantalla, estadoTeclado);
                }

                if (estadoMonito == EstadoObjeto.Cayendo)
                {
                    velocidadMonitoCayendo += gravedad * gameTime.ElapsedGameTime.Milliseconds / 1000f;
                    dx = (int)(velocidadMonitoCayendo * (gameTime.ElapsedGameTime.Milliseconds / 1000f) * pixelesXMetro);
                    monitoParado.Posicion = monitoEmpujando.Posicion = monitoCayendo.Posicion = monitoDeslizandose1.Posicion = monitoDeslizandose2.Posicion =
                        new Vector2(monitoParado.Posicion.X - velocidadMono * pixelesXMetro * gameTime.ElapsedGameTime.Milliseconds / 1000f, monitoParado.Posicion.Y + dx);
                    if (monitoParado.Posicion.Y > hoyito.Posicion.Y) estadoMonito = EstadoObjeto.Caido;
                }
                if (estadoMonito == EstadoObjeto.Deslizandose)
                {
                    velocidadMono = viMono - friccion * gravedad * masaMono * (tiempoTranscurrido / 1000f);
                    dx = (int)(velocidadMono * (gameTime.ElapsedGameTime.Milliseconds / 1000f) * pixelesXMetro);
                    monitoParado.Posicion = monitoEmpujando.Posicion = monitoCayendo.Posicion = monitoDeslizandose1.Posicion = monitoDeslizandose2.Posicion =
                        new Vector2(monitoParado.Posicion.X - dx, monitoParado.Posicion.Y);
                    if (velocidadMono <= 0) estadoMonito = EstadoObjeto.EnReposo;
                    if (monitoParado.Posicion.X + monitoParado.Ancho / 2 < piso.Posicion.X) estadoMonito = EstadoObjeto.Cayendo;
                }

                if ((estadoCaja == EstadoObjeto.EnReposo && estadoMonito == EstadoObjeto.EnReposo) ||
                    (estadoCaja == EstadoObjeto.EnReposo && estadoMonito == EstadoObjeto.Caido) ||
                    (estadoCaja == EstadoObjeto.Caido && estadoMonito == EstadoObjeto.Caido))
                {
                    tiempoCompletado += new TimeSpan(0, 0, opciones.dificultad == SistemaPerfiles.DificultadJuego.Fácil ? 5 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Medio ? 10 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Difícil ? 15 : 0);
                    if (opciones.sonidos != 0) mal.Play();
                    Reiniciar();
                    tiempoMalo = new TimeSpan(0, 0, 3);
                    tiempo.Color = Color.Red;
                }
                if (estadoMonito == EstadoObjeto.EnReposo && estadoCaja == EstadoObjeto.Caido)
                {
                    anotacion = new TimeSpan(0, 0, 3);
                    anotaciones.Color = Color.Navy;
                    if (opciones.sonidos != 0) bien.Play();
                    Reiniciar();
                    vecesCompletado++;
                    anotaciones.Texto = vecesCompletado.ToString();
                    if (vecesCompletado >= vecesRepetir) nivelCompletado = true;
                }
            }

            if ((opciones.invertir_mouse == 0 ? estadoRaton.LeftButton == ButtonState.Released : estadoRaton.RightButton == ButtonState.Released)
                && estadoCaja == EstadoObjeto.EnReposo && estadoMonito == EstadoObjeto.EnReposo) barraLlena.Escala = new Vector2(0, 1f);

            if (tiempoMalo >= TimeSpan.Zero) { tiempo.Actualizar(gameTime); tiempoMalo -= gameTime.ElapsedGameTime; }
            else { tiempo.Color = Color.Yellow; tiempo.Reiniciar(); }
            if (anotacion >= TimeSpan.Zero) { anotaciones.Actualizar(gameTime); anotacion -= gameTime.ElapsedGameTime; }
            else { anotaciones.Color = Color.Red; anotaciones.Reiniciar(); }

            estadoTecladoAnt = estadoTeclado;
            estadoRatonAnt = estadoRaton;
        }

        /// <summary><seealso cref="Nivel.Draw"/></summary>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (reproduciendoVideo) return;
            switch (estadoMonito)
            {
                case EstadoObjeto.Deslizandose:
                    if (velocidadMono > 4f) monitoDeslizandose1.Dibujar(spriteBatch, gameTime);
                    else monitoDeslizandose2.Dibujar(spriteBatch, gameTime);
                    break;
                case EstadoObjeto.Cayendo:
                    monitoCayendo.Dibujar(spriteBatch, gameTime);
                    break;
                case EstadoObjeto.Empujando:
                    monitoEmpujando.Dibujar(spriteBatch, gameTime);
                    break;
                default:
                    monitoParado.Dibujar(spriteBatch, gameTime);
                    break;
            }
            tiempo.Dibujar(spriteBatch, gameTime);
            anotaciones.Dibujar(spriteBatch, gameTime);
            mensaje.Dibujar(spriteBatch, gameTime);
            barraVacia.Dibujar(spriteBatch, gameTime);
            barraLlena.Dibujar(spriteBatch, gameTime);

            caja.Dibujar(spriteBatch, gameTime);
            piso.Dibujar(spriteBatch, gameTime);
            hoyito.Dibujar(spriteBatch, gameTime);

            distancia.Dibujar(spriteBatch, gameTime);
            lineaDistancia.Dibujar(spriteBatch, gameTime);
            cursor.Dibujar(spriteBatch, gameTime);
            spriteBatch.End();
        }
    }
}
