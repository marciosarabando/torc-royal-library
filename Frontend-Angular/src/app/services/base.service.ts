import { LocalStorageUtils } from '../utils/localstorage';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { throwError } from 'rxjs';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment.development';
import { ObjectToQueryParams } from '../core/mappers/helper/objectToQueryParams.helper';
import { lastValueFrom } from 'rxjs';
import { Injectable } from '@angular/core';
import _ from 'lodash';
import { LoaderService } from './interface/loader.service';

//Classe Abstrata de BASE SERVICE para Métodos usados por vários Services
@Injectable({
    providedIn: 'root',
})
export abstract class BaseService {
    private _endpoint: string = '';

    public get endpoint(): string {
        return this._endpoint;
    }
    public set endpoint(value: string) {
        this.aditionalUri = '';
        this._endpoint = value;
    }
    aditionalUri: string = '';

    public LocalStorage = new LocalStorageUtils();
    protected UrlServiceV1: string = environment.API;
    protected RouterBase: Router | undefined;

    constructor(
        private objectToQueryParams: ObjectToQueryParams,
        private http: HttpClient,
        private loaderService: LoaderService
    ) { }

    get<InputDto, OutputDto>(
        data?: { queryParamsData?: InputDto | any; uriData?: InputDto | any },
        oldWay?: boolean
    ): Promise<OutputDto> {
        let queryParamsData = this.objectToQueryParams.execute<InputDto>(
            data?.queryParamsData
        );
        const uriData = data?.uriData;
        let finalDataUri: string = this.normalizeUriData(uriData);

        queryParamsData = queryParamsData
            ? `${oldWay ? '&' : '?'}${queryParamsData}`
            : '';
        const url = `${this.UrlServiceV1}${this.endpoint}${this.aditionalUri}${finalDataUri}${queryParamsData}`;
        this.loader(url);
        return lastValueFrom(
            this.http.get<OutputDto>(url, { headers: this.setHeaders() })
        );
    }

    normalizeUriData(uriData: any): string {
        let uriDataAsUri: Array<any> = [];

        if (uriData) {
            if (_.isObject(uriData)) {
                Object.entries(uriData).forEach((key) => {
                    uriDataAsUri.push(key[1]);
                });
                return uriDataAsUri.join('/');
            }

            if (_.isArray(uriData)) {
                return uriData.join('/');
            }

            return `${String(uriData)}/`;
        }

        return '';
    }

    loader(url: string) {
        this.loaderService.addCounter(url);
        this.loaderService.showOverlay.next(true);
    }

    setHeaders(): HttpHeaders {
        return new HttpHeaders();
        // return new HttpHeaders().set("Authorization", "Bearer " + this.token.getToken())
        //   .append('Cache-Control', 'no-cache, no-store, must-revalidate, post-check=0, pre-check=0')
    }

    protected obterHeaderJson() {
        return {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        }
    }

    protected ObterAuthHeaderJson() {
        return {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${this.LocalStorage.obterTokenUsuario()}`
            })
        };
    }

    protected extractData(response: any) {
        return response || {};
    }

    protected serviceError(response: Response | any) {
        let customError: string[] = [];

        if (response instanceof HttpErrorResponse) {

            if (response.statusText === "Unknown Error") {
                customError.push("Ocorreu um erro desconhecido");
                response.error.errors = customError;
            }
        }

        console.error(response);
        return throwError(response);
    }
}