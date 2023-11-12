export class AplicacionDto {
  id: number;
  descripcion: string;
  url: string;
  origen: string; 
  clasificacion_ens: string;

  constructor(
    id: number,
    descripcion: string,
    url: string,
    origen: string,
    clasificacion_ens: string) {
    this.id = id;
    this.descripcion = descripcion;
    this.url = url;
    this.origen = origen;
    this.clasificacion_ens = clasificacion_ens;
  }
}
