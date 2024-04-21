export abstract class ObjectToQueryParams {
  abstract execute<InputDto>(data: InputDto): string;
}

export class ObjectToQueryParamsImplementation implements ObjectToQueryParams {
  execute<InputDto>(data: InputDto): string {
    if (!data) {
      return '';
    }
    return Object.keys((data as object))
      .map(key => `${key}=${(data as { [key: string]: string })[key]}`)
      .join('&');

  }
}

