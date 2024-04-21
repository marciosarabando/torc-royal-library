export class LocalStorageUtils {

  public obterTokenUsuario(): string {
    return String(localStorage.getItem("inspec.token"));
  }

  public salvarTokenUsuario(token: string) {
    localStorage.setItem("inspec.token", token);
  }

  public salvarUsuario(user: any) {
    localStorage.setItem("inspec.user", JSON.stringify(user));
  }

  public obterUsuario() {
    return JSON.parse(String(localStorage.getItem("inspec.user")));
  }

  public limparDadosLocaisUsuario() {
    localStorage.removeItem("inspec.token");
    localStorage.removeItem("inspec.user");
  }
}
