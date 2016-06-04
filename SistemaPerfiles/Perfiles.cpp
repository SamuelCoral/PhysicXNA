#include "Perfiles.h"
#include <stdio.h>	// FILE, NULL
#include <stdlib.h>	// malloc
#include <string.h>	// strcmp, memcpy


bool EsPerfilCorrecto(struct PerfilJugador* perfil) {

	if(perfil == NULL) return false;
	int c;
	for(c = 0; c < TAM_NOMBRES; c++) if(!perfil->opciones.nombre[c]) break;
	if(c == TAM_NOMBRES || !c) return false;
	if(perfil->opciones.nivel >= NUM_NIVELES) return false;
	if(perfil->opciones.dificultad > 2) return false;

	for(c = 0; c < NUM_NIVELES; c++)
		if(perfil->puntuaciones[c].centisegundos >= 100 ||
			perfil->puntuaciones[c].segundos >= 60 ||
			perfil->puntuaciones[c].minutos >= 100)
			return false;

	return true;
}


int ContarNodosPerfilLista(struct NodoPerfil* perfiles) {

	if(perfiles == NULL) return 0;
	int c;
	for(c = 1; perfiles->siguiente != NULL; c++, perfiles = perfiles->siguiente);
	return c;
}


int BuscarNodoPerfil(struct NodoPerfil* perfiles, const char* nombre) {

	bool encontrado = false;
	int c = 1;
	while(perfiles != NULL) {

		if(!strcmp(nombre, perfiles->perfil->opciones.nombre)) {

			encontrado = true;
			break;
		}

		c++;
		perfiles = perfiles->siguiente;
	}

	if(encontrado) return c;
	else return 0;
}


struct PerfilJugador* VerPerfilLista(struct NodoPerfil* perfiles, int lugar) {

	perfiles = VerNodoLista(perfiles, lugar);
	if(perfiles == NULL) return NULL;
	return perfiles->perfil;
}


struct NodoPerfil* VerNodoLista(struct NodoPerfil* perfiles, int lugar) {

	if(lugar < 1) return NULL;
	if(perfiles == NULL) return NULL;
	int c = 1;
	while(c < lugar) {

		c++;
		perfiles = perfiles->siguiente;
		if(perfiles == NULL) return NULL;
	}
	
	return perfiles;
}


bool IntercambiarPerfiles(struct NodoPerfil** perfiles, int primero, int segundo) {

	if(perfiles == NULL) return false;
	if(primero == segundo) return true;
	if(primero > segundo) {

		int auxiliar = primero;
		primero = segundo;
		segundo = auxiliar;
	}

	struct NodoPerfil* punteroPrimero = VerNodoLista(*perfiles, primero);
	struct NodoPerfil* punteroSegundo = VerNodoLista(*perfiles, segundo);

	if(punteroPrimero == NULL || punteroSegundo == NULL) return false;

	struct NodoPerfil* nodoAnteriorPrimero = VerNodoLista(*perfiles, primero - 1);
	struct NodoPerfil* nodoSiguienteSegundo = punteroSegundo->siguiente;

	if(primero + 1 == segundo) punteroSegundo->siguiente = punteroPrimero;
	else {
		
		struct NodoPerfil* nodoAnteriorSegundo = VerNodoLista(*perfiles, segundo - 1);
		struct NodoPerfil* nodoSiguientePrimero = punteroPrimero->siguiente;

		punteroSegundo->siguiente = nodoSiguientePrimero;
		nodoAnteriorSegundo->siguiente = punteroPrimero;
	}

	punteroPrimero->siguiente = nodoSiguienteSegundo;
	if(nodoAnteriorPrimero == NULL) *perfiles = punteroSegundo;
	else nodoAnteriorPrimero->siguiente = punteroSegundo;

	return true;
}


bool ActualizarPerfilLista(struct NodoPerfil* perfiles, struct PerfilJugador* perfil, int lugar) {

	if(perfil == NULL) return false;
	struct PerfilJugador* destino = VerPerfilLista(perfiles, lugar);
	if(destino == NULL) return false;
	memcpy(destino, perfil, sizeof(struct PerfilJugador));
	return true;
}


bool AgregarNodoPerfil(struct NodoPerfil** perfiles, struct PerfilJugador* nuevo) {

	if(perfiles == NULL) return false;
	if(!EsPerfilCorrecto(nuevo)) return false;
	if((*perfiles) == NULL) {

		(*perfiles) = (struct NodoPerfil*)malloc(sizeof(struct NodoPerfil));
		if((*perfiles) == NULL) return false;
		(*perfiles)->perfil = (struct PerfilJugador*)malloc(sizeof(struct PerfilJugador));
		if((*perfiles)->perfil == NULL) {
			
			free((*perfiles));
			(*perfiles) = NULL;
			return false;
		}

		memcpy((*perfiles)->perfil, nuevo, sizeof(struct PerfilJugador));
		(*perfiles)->siguiente = NULL;
	}

	else {

		struct NodoPerfil* recorrido = (*perfiles);
		while(recorrido->siguiente != NULL) recorrido = recorrido->siguiente;

		recorrido->siguiente = (struct NodoPerfil*)malloc(sizeof(struct NodoPerfil));
		if(recorrido->siguiente == NULL) return false;
		recorrido->siguiente->perfil = (struct PerfilJugador*)malloc(sizeof(struct PerfilJugador));
		if(recorrido->siguiente->perfil == NULL) {

			free(recorrido->siguiente);
			recorrido->siguiente = NULL;
			return false;
		}

		memcpy(recorrido->siguiente->perfil, nuevo, sizeof(struct PerfilJugador));
		recorrido->siguiente->siguiente = NULL;
	}

	return true;
}


bool EliminarNodoPerfil(struct NodoPerfil** perfiles, int lugar) {
	
	if(perfiles == NULL) return false;
	if(lugar < 1) return false;
	if((*perfiles) == NULL) return false;
	if((*perfiles)->siguiente == NULL && lugar > 1) return false;

	if(lugar == 1) {

		struct NodoPerfil* auxiliar = (*perfiles)->siguiente;
		free((*perfiles)->perfil);
		free((*perfiles));
		(*perfiles) = auxiliar;
	}

	else {

		struct NodoPerfil *recorrido = (*perfiles), *auxiliar;
		int c = 2;
		while(c < lugar) {

			c++;
			recorrido = recorrido->siguiente;
			if(recorrido->siguiente == NULL) return false;
		}

		auxiliar = recorrido->siguiente->siguiente;
		free(recorrido->siguiente->perfil);
		free(recorrido->siguiente);
		recorrido->siguiente = auxiliar;
	}

	return true;
}


struct NodoPerfil* CargarPerfilesArchivo(const char* ruta) {

	FILE* archivo;
	fopen_s(&archivo, ruta, "rb");
	if(archivo == NULL) return NULL;

	int tam_num_magico = strlen(NUM_MAGICO);
	char* buffer = (char*)calloc(tam_num_magico + 1, 1);
	if(buffer == NULL) { fclose(archivo); return NULL; }
	if(!fread(buffer, tam_num_magico, 1, archivo)) { free(buffer); fclose(archivo); return NULL; }
	if(strcmp(buffer, NUM_MAGICO)) { free(buffer); fclose(archivo); return NULL; }
	free(buffer);

	fseek(archivo, 0, SEEK_END);
	if((ftell(archivo) - tam_num_magico) % sizeof(struct PerfilJugador)) { fclose(archivo); return NULL; }
	fseek(archivo, tam_num_magico, SEEK_SET);

	struct NodoPerfil* perfiles = NULL;
	struct PerfilJugador auxiliar;
	while(true) {

		if(!fread(&auxiliar, sizeof(struct PerfilJugador), 1, archivo)) break;
		if(!AgregarNodoPerfil(&perfiles, &auxiliar)) {
			
			EliminarListaPerfiles(perfiles);
			fclose(archivo);
			return NULL;
		}
	}

	fclose(archivo);
	return perfiles;
}


bool GuardarPerfilesArchivo(struct NodoPerfil* perfiles, const char* ruta) {

	FILE* archivo;
	fopen_s(&archivo, ruta, "wb");
	if(archivo == NULL) return false;

	fwrite(NUM_MAGICO, strlen(NUM_MAGICO), 1, archivo);
	while(perfiles != NULL) {

		fwrite(perfiles->perfil, sizeof(struct PerfilJugador), 1, archivo);
		perfiles = perfiles->siguiente;
	}

	fclose(archivo);
	return true;
}


void EliminarListaPerfiles(struct NodoPerfil* perfiles) {

	if(perfiles == NULL) return;
	struct NodoPerfil* recorrido;
	while(perfiles->siguiente != NULL) {

		recorrido = perfiles;
		while(recorrido->siguiente->siguiente != NULL) recorrido = recorrido->siguiente;
		free(recorrido->siguiente->perfil);
		free(recorrido->siguiente);
		recorrido->siguiente = NULL;
	}

	free(perfiles->perfil);
	free(perfiles);
}

