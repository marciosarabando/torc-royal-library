import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BookResponse } from 'src/app/models/course/course.dto';
import { BookService } from 'src/app/services/book/book.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  providers: [BookService]
})
export class HomeComponent {

  books: BookResponse[] | undefined;
  formulario: FormGroup | undefined;
  submited: boolean = false;

  constructor(
    private courseService: BookService,
    private formBuilder: FormBuilder,
  ) {

    this.formulario = this.formBuilder.group({
      searchValue: [null, [Validators.required, Validators.minLength(1), Validators.maxLength(150)]],
      search: ["Select", [Validators.required, Validators.pattern('[0-9]')]]
    })

    this.courseService.getAll(null, null).then((data) => {
      this.books = data;
    });
  }

  onSubmit() {
    this.submited = true;
    if (this.formulario.invalid)
      return;

    console.log(this.formulario.value)

    console.log(this.formulario.value.search);

    this.courseService.getAll(this.formulario.value.search, this.formulario.value.searchValue).then((data) => {
      this.books = data;
    });

    //this.formulario.reset();
    this.submited = false;
  }

}
