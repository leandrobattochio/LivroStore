import { Guid } from "guid-typescript";

export class Livro {
    id!: Guid;
    imagemCapa!: string;
    titulo!: string;
    isbn!:  string;
    editora!: string;
    autor!: string;
    sinopse!: string;
    dataPublicacao!: Date;
}


export class RegistrarUsuarioCommand {
    usuario!: string;
    senha!: string;
    email!: string;
    tipousuario!: number;
}

export class LoginUsuarioCommand {
    usuario!: string;
    senha!: string;
}