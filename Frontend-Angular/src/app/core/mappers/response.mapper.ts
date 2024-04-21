export class ReponseMapper {
  static map<CustomResponse>(
    req: Promise<any>,
    old = false
  ): Promise<CustomResponse> {
    return new Promise<CustomResponse>((resolve, reject) => {
      req
        .then((result) => {
          if (old) {
            resolve(result);
          } else {
            if (!result.success) {
              reject(result.message);
            }
            resolve(result.data);
          }
        })
        .catch((error) => {
          reject(error);
        });
    });
  }
}
