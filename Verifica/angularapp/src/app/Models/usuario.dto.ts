export class UsuarioDto {
  id?: number;
  nombre?: string;
  apellido1: string;
  apellido2: string;
  username: string;
  password: string;
  guid: string;
  email: string;
  telefono: string;
  admin: boolean;

  constructor(
    nombre: string,
    apellido1: string,
    apellido2: string,
    username: string,
    password: string,
    guid: string,
    email: string,
    telefono: string,
    admin: boolean,
  ) {
    this.nombre = nombre;
    this.apellido1 = apellido1;
    this.apellido2 = apellido2;
    this.username = username;
    this.password = password;
    this.guid = guid;
    this.email = email;
    this.telefono = telefono;
    this.admin = admin;
  }
}
