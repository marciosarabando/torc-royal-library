import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { ReponseMapper } from 'src/app/core/mappers/response.mapper';
import { BookResponse as BookResponse } from 'src/app/models/course/course.dto';

@Injectable()
export class BookService extends BaseService {

  public getAll(searchBy, value): Promise<BookResponse[] | undefined> {
    this.endpoint = `/book/getallbysearchtype`;
    if (searchBy != null)
      this.aditionalUri = `?searchBy=${searchBy}&value=${value}`

    return ReponseMapper.map<BookResponse[] | undefined>(
      this.get<null, BookResponse[] | undefined>()
    );
  }
}