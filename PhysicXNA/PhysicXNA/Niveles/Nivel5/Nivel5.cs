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
    /// <summary>Escenario del Nivel 5.</summary>
    public class Nivel5 : Nivel
    {
        internal DuendeEstatico fondo;
        internal NaveEspacial nave;
        internal static float pixelesXMetro;
        internal static double KiloGramosXMetroCubico;
        internal static double CGU = 6.67408E-11;
        /// <summary>Especifica el número de meteoritos máximos que pueden aparecer en el escenario en esta partida.</summary>
        public int numMeteoritos;
        int meteoritosDestruidos;
        int meteoritosDestruir;
        int daño;
        List<Proyectil> proyectiles;
        List<Meteorito> meteoritos;
        List<iGravitatorio> objetosFlotando;
        List<Explosion> explosiones;
        DuendeEstatico puntero;
        Random genNumAleatorios;

        SoundEffect sonidoExplosion;
        SoundEffect sonidoLaser;
        Texto3D tiempo;
        Texto3D mensaje;
        Texto3D anotaciones;
        TimeSpan tiempoMalo;
        TimeSpan anotacion;

        /// <summary>
        /// Construye una instancia para el nivel 5.
        /// </summary>
        /// <param name="juego">Juego en que será mostrado el nivel.</param>
        /// <param name="rutaVideo">Ruta del archivo de video de introducción al nivel.</param>
        /// <param name="rutaMusica">Ruta del archivo de música que será reproducido en el nivel.</param>
        /// <param name="cuadrosPorSegundo"><seealso cref="Nivel.cuadrosPorSegundo"/></param>
        public Nivel5(Game juego, String rutaMusica, String rutaVideo, int cuadrosPorSegundo)
            : base(juego, rutaMusica, rutaVideo, cuadrosPorSegundo)
        {
            reglas[0] =
                "Muévete en el espacio para encontrar a los meteoritos\n" +
                "y dispárales para destruirlos, guíate por las flechas.\n" +
                "Cuidado que todos los meteoritos tienen fuerza de gravedad sobre\n" +
                "ellos y sobre tí, si chocas serás penalizado con tiempo.";
            reglas[1] =
                "Flechas o WASD                    - Moverse en el escenario\n" +
                "Click principal                      - Disparar\n" +
                "Click secundario o Shift   - Acelerar más\n" +
                "Control                                  - Acelerar menos";
            reglas[2] =
                "                                                        /* Gravedad *\\\n\n" +
                "La fuerza de gravedad es una de las 4 fuerzas fundamentales que rigen\n" +
                "el comportamiento de nuestro universo, por lo que es una de las mas\n" +
                "estudiadas y que además está presente en cada partícula subatómica.";

            fondo = new DuendeEstatico("Fondos/fractal", Vector2.Zero);
            nave = new NaveEspacial(this);
            proyectiles = new List<Proyectil>();
            meteoritos = new List<Meteorito>();
            objetosFlotando = new List<iGravitatorio>();
            explosiones = new List<Explosion>();
            puntero = new DuendeEstatico("Recursos/Nivel5/puntero", Vector2.Zero, escala: new Vector2(0.15f), origenGiro: new Vector2(162, 62));

            tiempo = new Texto3D("Fuentes/TextoTiempo", "", new Vector2(10, 10), 30, new Color(172, 228, 255, 255));
            mensaje = new Texto3D("Fuentes/TextoMensaje", "Dispara y destruye a los meteoritos.", new Vector2(10, 50), 30, new Color(172, 228, 255, 255));
            anotaciones = new Texto3D("Fuentes/TextoAnotaciones", String.Empty, Vector2.Zero, 30, Color.Red, 3);

            pixelesXMetro = 10f;
            KiloGramosXMetroCubico = 3000;
            genNumAleatorios = new Random();
        }
        /// <summary><seealso cref="Nivel.LoadContent"/></summary>
        protected override void LoadContent()
        {
            fondo.CargarContenido(Content);
            nave.CargarContenido(Content);
            nave.flecha.CargarContenido(Content);
            puntero.CargarContenido(Content);
            sonidoExplosion = Content.Load<SoundEffect>("Sonidos/boom");
            sonidoLaser = Content.Load<SoundEffect>("Sonidos/laser");
            tiempo.CargarContenido(Content);
            mensaje.CargarContenido(Content);
            anotaciones.CargarContenido(Content);
            base.LoadContent();
        }

        /// <summary><seealso cref="Nivel.UnloadContent"/></summary>
        protected override void UnloadContent()
        {
            fondo.LiberarContenido();
            nave.LiberarContenido();
            nave.flecha.LiberarContenido();
            puntero.LiberarContenido();
            foreach (Proyectil proyectil in proyectiles) proyectil.LiberarContenido();
            foreach (Meteorito meteorito in meteoritos)
            {
                meteorito.flecha.LiberarContenido();
                meteorito.LiberarContenido();
            }
            sonidoExplosion.Dispose();
            sonidoLaser.Dispose();
            tiempo.LiberarContenido();
            mensaje.LiberarContenido();
            anotaciones.LiberarContenido();
            base.UnloadContent();
        }

        private void Reiniciar()
        {
            proyectiles.Clear();
            meteoritos.Clear();
            objetosFlotando.Clear();
            objetosFlotando.Add(nave);
            nave.posicionAbsoluta = new Vector2((fondo.Ancho - nave.Ancho) / 2f, (fondo.Alto - nave.Alto) / 2f);
            nave.velocidad = Vector2.Zero;
        }

        /// <summary><seealso cref="Nivel.Inicializar"/></summary>
        public override void Inicializar()
        {
            base.Inicializar();
            dibujarAntes.Add(fondo);

            fondo.Escala = new Vector2(5f);
            nave.flecha.OrigenGiro = new Vector2((float)nave.flecha.AnchoReal, nave.flecha.AltoReal / 2f);

            anotacion = TimeSpan.Zero;
            numMeteoritos = opciones.dificultad == SistemaPerfiles.DificultadJuego.Fácil ? 7 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Medio ? 12 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Difícil ? 15 : 1000;
            daño = opciones.dificultad == SistemaPerfiles.DificultadJuego.Fácil ? 20 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Medio ? 15 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Difícil ? 10 : 1000;
            meteoritosDestruir = opciones.dificultad == SistemaPerfiles.DificultadJuego.Fácil ? 10 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Medio ? 15 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Difícil ? 20 : 1000;
            meteoritosDestruidos = 0;
            anotaciones.Texto = "0";
            anotaciones.Posicion = new Vector2(botonAyuda.Posicion.X - 30 - anotaciones.TamTexto.X, 10);

            Reiniciar();
        }

        /// <summary><seealso cref="Nivel.Update"/></summary>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (reproduciendoVideo) return;

            tiempo.Texto = tiempoCompletado.Minutes.ToString("00") + ":" + tiempoCompletado.Seconds.ToString("00");
            puntero.Posicion = new Vector2(estadoRaton.X, estadoRaton.Y);

            while (meteoritos.Count < numMeteoritos)
            {
                Meteorito meteorito = new Meteorito(this);
                meteorito.CargarContenido(Content);
                meteorito.Escala = new Vector2(0.4f);
                meteorito.flecha.CargarContenido(Content);
                meteorito.flecha.Escala = new Vector2(0.3f);
                meteorito.flecha.OrigenGiro = new Vector2((float)meteorito.flecha.AnchoReal, meteorito.flecha.AltoReal / 2f);
                meteorito.Masa = Nivel5.KiloGramosXMetroCubico * Math.Pow(Nivel5.pixelesXMetro * meteorito.Ancho / 2d * pixelesXMetro, 3d) * Math.PI * 4 / 3;
                do meteorito.PosicionAbsoluta = new Vector2(genNumAleatorios.Next(fondo.Ancho - meteorito.Ancho), genNumAleatorios.Next(fondo.Alto - meteorito.Alto));
                while (Math.Abs((meteorito.PosicionAbsoluta - nave.PosicionAbsoluta).X) < resolucionPantalla.Width / 2 || Math.Abs((meteorito.PosicionAbsoluta - nave.PosicionAbsoluta).Y) < resolucionPantalla.Height / 2);
                meteorito.area = new Circulo(meteorito.PosicionAbsoluta + new Vector2(meteorito.Ancho / 2, meteorito.Alto / 2), meteorito.Ancho / 2);
                meteoritos.Add(meteorito);
                objetosFlotando.Add(meteorito);
            }

            foreach (iGravitatorio objeto in objetosFlotando)
                foreach (iGravitatorio objetoComprobar in objetosFlotando)
                    objetoComprobar.ObtenerIncrementoPosicion(objeto, gameTime.ElapsedGameTime);

            nave.Actualizar(gameTime, resolucionPantalla, estadoTeclado, estadoRaton);
			if (nave.Posicion.X < -800 || nave.Posicion.Y < -800 || nave.Posicion.X > resolucionPantalla.Width + 800 || nave.Posicion.Y > resolucionPantalla.Height + 800)
            {
                nave.posicionAbsoluta = new Vector2((fondo.Ancho - nave.Ancho) / 2f, (fondo.Alto - nave.Alto) / 2f);
                nave.velocidad = Vector2.Zero;
                if (opciones.sonidos != 0) sonidoExplosion.Play(1f, -1f, 0f);
                tiempoCompletado += new TimeSpan(0, 0, opciones.dificultad == SistemaPerfiles.DificultadJuego.Fácil ? 10 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Medio ? 15 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Difícil ? 20 : 0);
                tiempoMalo = new TimeSpan(0, 0, 3);
                tiempo.Color = Color.Red;
            }

            if (opciones.invertir_mouse == 0 ? estadoRaton.LeftButton == ButtonState.Pressed && estadoRatonAnt.LeftButton == ButtonState.Released : estadoRaton.RightButton == ButtonState.Pressed && estadoRatonAnt.RightButton == ButtonState.Released)
            {
                Vector2 posicionNuevoProyectil = nave.posicionAbsoluta + new Vector2(nave.Ancho / 2f, nave.Alto / 2f);
                double anguloGiroNuevoProyectil = Math.Atan2(estadoRaton.Y - (nave.Posicion.Y + nave.Alto / 2f), estadoRaton.X - (nave.Posicion.X + nave.Ancho / 2f));
                posicionNuevoProyectil += new Vector2((float)(nave.Ancho / 2f * Math.Cos(anguloGiroNuevoProyectil)), (float)(nave.Alto / 2f * Math.Sin(anguloGiroNuevoProyectil)));
                Proyectil nuevoProyectil = new Proyectil(this, posicionNuevoProyectil, anguloGiroNuevoProyectil);
                nuevoProyectil.CargarContenido(Content);
                nuevoProyectil.Escala = new Vector2(0.2f);
                nuevoProyectil.OrigenGiro = new Vector2(0, nuevoProyectil.AltoReal / 2f);
                proyectiles.Add(nuevoProyectil);
                if(opciones.sonidos != 0) sonidoLaser.Play();
                //objetosFlotando.Add(nuevoProyectil);
            }

            foreach (Proyectil proyectil in proyectiles)
            {
                proyectil.Actualizar(gameTime, resolucionPantalla, estadoTeclado);
                if (proyectil.posicionAbsoluta.X < -20 || proyectil.posicionAbsoluta.Y < -20 || proyectil.posicionAbsoluta.X > fondo.Ancho + 20 || proyectil.posicionAbsoluta.Y > fondo.Alto + 20)
                {
                    proyectiles.Remove(proyectil);
                    //objetosFlotando.Remove(proyectil);
                    break;
                }

                bool proyectilChocado = false;
                foreach (Meteorito meteorito in meteoritos)
                {
                    foreach (Circulo circulo in proyectil.area)
                    {
                        if (circulo.Intersecta(meteorito.area))
                        {
                            proyectilChocado = true;
                            meteorito.vida -= daño;
                            if (meteorito.vida <= 0)
                            {
                                Explosion explosion1 = new Explosion(this, meteorito.PosicionAbsoluta - new Vector2(meteorito.Ancho / 2f, 0));
                                explosion1.CargarContenido(Content);
                                explosion1.Escala = new Vector2((float)meteorito.Ancho / explosion1.AnchoReal) * 2f;
                                explosiones.Add(explosion1);
                                if (opciones.sonidos != 0) sonidoExplosion.Play(1f, -1f, 0f);
                                meteoritosDestruidos++;
                                anotaciones.Texto = meteoritosDestruidos.ToString();
                                anotacion = new TimeSpan(0, 0, 3);
                                anotaciones.Color = Color.Navy;
                                if (meteoritosDestruidos >= meteoritosDestruir) nivelCompletado = true;
                            }
                            else
                            {
                                Explosion explosion1 = new Explosion(this, circulo.centro - new Vector2(circulo.radio * 2f, 0));
                                explosion1.CargarContenido(Content);
                                explosion1.Escala = new Vector2(circulo.radio * 2f / explosion1.AnchoReal) * 2f;
                                explosiones.Add(explosion1);
                                if (opciones.sonidos != 0) sonidoExplosion.Play(1f, 1f, 0f);
                            }
                            break;
                        }
                    }

                    if (proyectilChocado) break;
                }

                if (proyectilChocado)
                {
                    proyectiles.Remove(proyectil);
                    objetosFlotando.Remove(proyectil);
                    break;
                }
            }

            foreach (Meteorito meteorito in meteoritos)
            {
                meteorito.Actualizar(gameTime, resolucionPantalla, estadoTeclado);
                if (meteorito.PosicionAbsoluta.X + meteorito.Ancho < -50 || meteorito.PosicionAbsoluta.Y + meteorito.Alto < -20 ||
                    meteorito.PosicionAbsoluta.X > fondo.Ancho + 50 || meteorito.PosicionAbsoluta.Y > fondo.Alto + 50)
                    meteorito.vida = 0;

                if (meteorito.area.Intersecta(nave.area))
                {
                    meteorito.vida = 0;
                    Explosion explosion1 = new Explosion(this, meteorito.PosicionAbsoluta - new Vector2(meteorito.Ancho / 2f, 0));
                    Explosion explosion2 = new Explosion(this, nave.PosicionAbsoluta - new Vector2(nave.Ancho / 2f, 0));
                    explosion1.CargarContenido(Content);
                    explosion2.CargarContenido(Content);
                    explosion1.Escala = new Vector2((float)meteorito.Ancho / explosion1.AnchoReal) * 2f;
                    explosion2.Escala = new Vector2((float)nave.Ancho / explosion2.AnchoReal) * 2f;
                    explosiones.Add(explosion1);
                    explosiones.Add(explosion2);
                    nave.posicionAbsoluta = new Vector2((fondo.Ancho - nave.Ancho) / 2f, (fondo.Alto - nave.Alto) / 2f);
                    nave.velocidad = Vector2.Zero;
                    if (opciones.sonidos != 0) sonidoExplosion.Play(1f, -1f, 0f);
                    tiempoCompletado += new TimeSpan(0, 0, opciones.dificultad == SistemaPerfiles.DificultadJuego.Fácil ? 5 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Medio ? 10 : opciones.dificultad == SistemaPerfiles.DificultadJuego.Difícil ? 15 : 0);
                    tiempoMalo = new TimeSpan(0, 0, 3);
                    tiempo.Color = Color.Red;
                }

                foreach (Meteorito otro in meteoritos)
                {
                    if (otro.Equals(meteorito)) continue;
                    if (otro.area.Intersecta(meteorito.area))
                    {
                        meteorito.vida = otro.vida = 0;
                        Explosion explosion1 = new Explosion(this, meteorito.PosicionAbsoluta - new Vector2(meteorito.Ancho / 2f, 0));
                        Explosion explosion2 = new Explosion(this, otro.PosicionAbsoluta - new Vector2(otro.Ancho / 2f, 0));
                        explosion1.CargarContenido(Content);
                        explosion2.CargarContenido(Content);
                        explosion1.Escala = new Vector2((float)meteorito.Ancho / explosion1.AnchoReal) * 2f;
                        explosion2.Escala = new Vector2((float)otro.Ancho / explosion2.AnchoReal) * 2f;
                        explosiones.Add(explosion1);
                        explosiones.Add(explosion2);
                        break;
                    }
                }

                if (meteorito.vida <= 0)
                {
                    meteoritos.Remove(meteorito);
                    objetosFlotando.Remove(meteorito);
                    break;
                }
            }

            foreach (Explosion explosion in explosiones)
            {
                explosion.Actualizar(gameTime, resolucionPantalla, estadoTeclado);
                if (explosion.desaparecer) { explosiones.Remove(explosion); break; }
            }

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
            foreach (Meteorito meteorito in meteoritos)
            {
                if (meteorito.dibujarFlecha) meteorito.flecha.Dibujar(spriteBatch, gameTime);
                else meteorito.Dibujar(spriteBatch, gameTime);
            }
            foreach (Explosion explosion in explosiones) explosion.Dibujar(spriteBatch, gameTime);
            if (nave.dibujarFlecha) nave.flecha.Dibujar(spriteBatch, gameTime);
            else nave.Dibujar(spriteBatch, gameTime);
            foreach (Proyectil proyectil in proyectiles) proyectil.Dibujar(spriteBatch, gameTime);
            tiempo.Dibujar(spriteBatch, gameTime);
            mensaje.Dibujar(spriteBatch, gameTime);
            anotaciones.Dibujar(spriteBatch, gameTime);
            puntero.Dibujar(spriteBatch, gameTime);
            spriteBatch.End();
        }
    }
}
