import { Inject, Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { LoaderService } from "./interface/loader.service";

@Injectable({
  providedIn: 'root'
})
export class LoaderServiceImplementation implements LoaderService {

  constructor() {
    this.showOverlay.next(false);
  }

  private _showOverlay = new Subject<boolean>();
  public get showOverlay() {
    return this._showOverlay;
  }
  public set showOverlay(value) {
    this._showOverlay = value;
  }

  private _counter: Array<string> = [];
  public get counter() {
    return this._counter;
  }

  addCounter(url: string) {
    this._counter.push(url);
  }

  removeCounter(url: string) {
    this._counter = this._counter.filter(counterUrl => counterUrl !== url);
  }
}
