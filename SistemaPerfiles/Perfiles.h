/****
*
*	Sistema de Perfiles de usuario
*	
* Este módulo tiene funciones para trabajar fácilmente con perfiles de usuario
* utilizados por el juego.
* Un perfil de usuario está constituido por su configuración del juego y sus
* puntuaciones de cada minijuego, además incluye su nombre, el último nivel
* alcanzado y si ya ha concluido el juego al menos una vez.
* Cada perfil de jugador debe tener un nombre diferente.
* 
* Las configuraciones que se almacenan son:
*  - Dificultad elegida
*  - Resolución de pantalla
*  - Pantalla completa activada o desactivada
*  - Sonidos activados o desactivados
*  - Música activada o desactivada
*  - Volumen de la música
*  - Utilizar WASD o flechas direccionales
*  - Invertir los botones del mouse
*
* En las puntuaciones de los minijuegos solo se incluye el tiempo en minutos,
* segundos y milésimas de segundo de en cuánto fué completado cada nivel.
*
****/



// Con esto evitamos errores en caso de que el encabezado llegue a ser incluido
// más de una vez en un mismo código
#ifndef __PERFILES_H__
#define __PERFILES_H__


// Definimos el uso de las funciones DLL para windows
// Al compilar esta librería las funciones deben ser utilizadas para ser exportadas
// Al implementar la librería deben usarse para ser importadas
#ifdef _WINDOWS
#	ifdef SISTEMAPERFILES_EXPORTS
#		define EXPORTAR_DLL __declspec(dllexport)
#	else
#		define EXPORTAR_DLL __declspec(dllimport)
#	endif
#else
#	define EXPORTAR_DLL
#endif


// Especificamos al enlazador que queremos utilizar el nombrado de funciones de C
// Aunque estemos trabajando o compilando en C++
// Esto para facilitarnos el trabajo al importar las funciones desde otro lenguaje
// Además en caso de que no estemos trabajando en C++ incluir la cabecera stdbool.h
#ifdef __cplusplus
extern "C" {
#else
#include <stdbool.h>
#endif


// Cabecera para utilizar enteros de tamaño exacto
#include <stdint.h>


// Número de niveles del juego
#define NUM_NIVELES 5
// Tamaño en caracteres de los nombres de jugador
#define TAM_NOMBRES 30
// Número mágico del archivo de perfiles
#define NUM_MAGICO "PJPX"



///////////////////////
//    ESTRUCTURAS    //
///////////////////////


// Definimos explícitamente el valor de alineamiento de las estructura para evitar
// problemas en otras plataformas, 8 es el default
#pragma pack(push, 8)


struct OpcionesJuego {

	char nombre[TAM_NOMBRES];
	uint8_t nivel;
	uint8_t juego_completado;
	uint8_t dificultad;

	// Opciones de video
	uint16_t res_horizontal;
	uint16_t res_vertical;
	uint8_t pantalla_completa;

	// Opciones de sonido
	uint8_t sonidos;
	uint8_t musica;
	uint8_t volumen;

	// Opciones de controles
	uint8_t wasd;
	uint8_t invertir_mouse;
};


struct PuntuacionesJuego {

	uint8_t minutos;
	uint8_t segundos;
	uint8_t centisegundos;
};


struct PerfilJugador {

	struct OpcionesJuego opciones;
	struct PuntuacionesJuego puntuaciones[NUM_NIVELES];
};


struct NodoPerfil {

	struct PerfilJugador* perfil;
	struct NodoPerfil* siguiente;
};


#pragma pack(pop)



/////////////////////
//    FUNCIONES    //
/////////////////////


/* 
 * Carga los perfiles de jugador del archivo especificado.
 * 
 *  --- PARÁMETROS ---
 *  
 *  ruta	- Nombre y ruta del archivo a cargar
 * 
 */
EXPORTAR_DLL struct NodoPerfil* __cdecl CargarPerfilesArchivo(const char* ruta);


/* 
 * Guarda los perfiles de jugador al archivo especificado.
 * Devuelve false en caso de error.
 * 
 *  --- PARÁMETROS ---
 *  
 *  perfiles	- Lista de perfiles a guardar
 *  ruta		- Nombre y ruta del archivo a guardar
 * 
 */
EXPORTAR_DLL bool __cdecl GuardarPerfilesArchivo(struct NodoPerfil* perfiles, const char* ruta);


/* 
 * Agrega un nodo al final de la lista de perfiles de jugador especificada.
 * Devuelve false en caso de error.
 * 
 *  --- PARÁMETROS ---
 *  
 *  perfiles	- Lista de perfiles donde agregar el nuevo nodo
 *  nuevo		- Perfil a agregar a la lista
 * 
 */
EXPORTAR_DLL bool __cdecl AgregarNodoPerfil(struct NodoPerfil** perfiles, struct PerfilJugador* nuevo);


/* 
 * Elimina el nodo en el lugar especificado de la lista de perfiles de jugador.
 * Devuelve false en caso de error.
 * 
 *  --- PARÁMETROS ---
 *  
 *  perfiles	- Lista de perfiles de la cual eliminar el perfil
 *  lugar		- Posición en la lista que ocupa el nodo a eliminar comenzando en 1
 * 
 */
EXPORTAR_DLL bool __cdecl EliminarNodoPerfil(struct NodoPerfil** perfiles, int lugar);


/* 
 * Devuelve la dirección de memoria del perfil de jugador en el lugar indicado
 * de una lista de perfiles para acceder a sus datos en modo lectura y escritura.
 * En caso de que no sea encontrado, la función devolverá NULL.
 * 
 *  --- PARÁMETROS ---
 *  
 *  perfiles	- Lista de perfiles de la que se tomará el perfil
 *  lugar		- Posición en la lista que ocupa el nodo a tomar comenzando en 1
 * 
 */
EXPORTAR_DLL struct PerfilJugador* __cdecl VerPerfilLista(struct NodoPerfil* perfiles, int lugar);


/*
 * Devuelve la dirección de memoria del nodo del perfil de jugador en el lugar indicado
 * de la lista de perfiles de jugador para acceder a sus datos en modo lectura y escritura.
 * En caso de que no sea encontrado, la función devolverá NULL.
 * 
 *  --- PARÁMETROS ---
 *  
 *  perfiles	- Lista de perfiles de la que se tomará el nodo
 *  lugar		- Posición en la lista que ocupa el nodo a tomar comenzando en 1
 * 
 */
EXPORTAR_DLL struct NodoPerfil* __cdecl VerNodoLista(struct NodoPerfil* perfiles, int lugar);


/* 
 * Intercambia el lugar en la lista que ocupan 2 perfiles de jugador.
 * Devuelve false en caso de error.
 * 
 *  --- PARÁMETROS ---
 *  
 *  perfiles	- Lista que contiene los perfiles a intercambiar
 *  primero		- Lugar en la lista del primer perfil
 *  segundo		- Lugar en la lista del segundo perfil
 * 
 */
EXPORTAR_DLL bool __cdecl IntercambiarPerfiles(struct NodoPerfil** perfiles, int primero, int segundo);


/* 
 * Sobreescribe la información del perfil en el lugar de la lista especificada
 * por la información del nuevo perfil.
 * En caso de que no sea encontrado, la función devolverá false.
 * 
 *  --- PARÁMETROS ---
 *  
 *  perfiles	- Lista de perfiles en que se sobreescribirá el nuevo perfil
 *  perfil		- Perfil con la información a sobreescribir
 *  lugar		- Posición en la lista que ocupa el nodo a sobreescribir comenzando en 1
 * 
 */
EXPORTAR_DLL bool __cdecl ActualizarPerfilLista(struct NodoPerfil* perfiles, struct PerfilJugador* perfil, int lugar);


/* 
 * Busca un perfil de jugador por nombre en la lista especificada.
 * Devuelve el lugar que ocupa en la lista comenzando en 1.
 * En caso de que no sea encontrado, la función devolverá 0.
 * 
 *  --- PARÁMETROS ---
 *  
 *  perfiles	- Lista de perfiles en la que se buscará el perfil
 *  nombre		- Nombre del perfil a buscar
 * 
 */
EXPORTAR_DLL int __cdecl BuscarNodoPerfil(struct NodoPerfil* perfiles, const char* nombre);


/* 
 * Cuenta el número de nodos almacenados en la lista de perfiles especificada.
 * 
 *  --- PARÁMETROS ---
 *  
 *  perfiles	- Lista de perfiles de la que se contarán los elementos
 * 
 */
EXPORTAR_DLL int __cdecl ContarNodosPerfilLista(struct NodoPerfil* perfiles);


/* 
 * Libera la memoria ocupada por todos los nodos de una lista de perfiles.
 * NOTA: Si desea reciclar el mismo puntero más tarde, es necesario asignar NULL
 *       al puntero después de llamar a esta función.
 *
 *  --- PARÁMETROS ---
 *  
 *  perfiles	- Lista de perfiles a ser eliminada
 * 
 */
EXPORTAR_DLL void __cdecl EliminarListaPerfiles(struct NodoPerfil* perfiles);


/* 
 * Verifica que la información almacenada en el perfil de jugador no esté corrupta.
 * 
 *  --- PARÁMETROS ---
 *  
 *  perfil	- Perfil de jugador a verificar
 * 
 */
EXPORTAR_DLL bool __cdecl EsPerfilCorrecto(struct PerfilJugador* perfil);


#ifdef __cplusplus
}
#endif

#endif /* __PERFILES_H__ */

