export class AplicacionDto {
  id?: number;
  descripcion: string;
  url: string;
  origen: string; 
  clasificacion_ens: string;

  constructor(
    descripcion: string,
    url: string,
    origen: string,
    clasificacion_ens: string) {
    this.descripcion = descripcion;
    this.url = url;
    this.origen = origen;
    this.clasificacion_ens = clasificacion_ens;
  }
}
