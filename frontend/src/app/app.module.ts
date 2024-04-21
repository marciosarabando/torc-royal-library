import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './modules/common/home/home.component';
import { HttpClientModule } from '@angular/common/http';
import { BookService } from './services/book/book.service';
import { ObjectToQueryParams, ObjectToQueryParamsImplementation } from './core/mappers/helper/objectToQueryParams.helper';
import { LoaderServiceImplementation } from './services/loader.service';
import { LoaderService } from './services/interface/loader.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [BookService,
    {
      provide: ObjectToQueryParams,
      useClass: ObjectToQueryParamsImplementation
    },
    {
      provide: LoaderService,
      useClass: LoaderServiceImplementation
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
