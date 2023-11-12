export class Token2FADto {
  id: number;
  creado: Date;
  idusuario: number;
  idaplicacion: number;
  token: string;
  aceptado: boolean;
  rechazado: boolean;

  constructor(
    id: number,
    creado: Date,
    idusuario: number,
    idaplicacion: number,
    token: string,
    aceptado: boolean,
    rechazado: boolean) {
      this.id = id;
      this.creado = creado;
      this.idusuario = idusuario;
      this.idaplicacion = idaplicacion
      this.token = token;
      this.aceptado = aceptado;
      this.rechazado = rechazado;
    }
}

