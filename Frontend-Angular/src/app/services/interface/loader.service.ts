import { Inject, Injectable } from "@angular/core";
import { Subject } from "rxjs";


@Injectable({
    providedIn: 'root'
})
export abstract class LoaderService {
    abstract showOverlay: Subject<boolean>;
    abstract addCounter(url: string): void;
    abstract removeCounter(url: string): void;
    abstract counter: Array<string>;
}