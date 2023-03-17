import { EndpointFactory } from 'src/environments/endpoint.factory';
import { Injectable, Injector } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ConfigurationService } from '../services/configuration.service'
import { NumerologyPortrait } from 'src/models/numerologyPortrait.model';
import { NumerologyNameModel } from 'src/models/numerologyName.model';

@Injectable()
export class NumerologyEndpoint extends EndpointFactory {
  private readonly _nameCalculationUrl: string = "/api/numerology/numerologyName/";
  private readonly _nameCalculationWholeUrl: string = "/api/numerology/numerologyNames/";
  private readonly _clientPortraitUrl: string = "/api/numerology/portrait/";
  private readonly _addNewPortraitUrl: string = "/api/numerology/addPortrait";
  private readonly _numerologyBirthDate: string = "/api/numerology/numerologyDate/";
  private readonly _numerologyBirthDateTree: string = "/api/numerology/numerologyDateTree/";
  private readonly _numerologyPrint: string = "/api/numerology/printPortrait";
  private readonly _getNumerologyPrint: string = "/api/numerology/getPrint/";
  private readonly _deleteNumerologyPrint: string = "/api/numerology/deletePortrait/";

  get nameCalculationUrl() { return this.configuration.baseUrl + this._nameCalculationUrl; }
  get nameCalculationWholeUrl() { return this.configuration.baseUrl + this._nameCalculationWholeUrl; }
  get clientPortraitUrl() { return this.configuration.baseUrl + this._clientPortraitUrl; }
  get addNewPortraitUrl() { return this.configuration.baseUrl + this._addNewPortraitUrl; }
  get numerologyBirthDate() { return this.configuration.baseUrl + this._numerologyBirthDate; }
  get numerologyBirthDateTree() { return this.configuration.baseUrl + this._numerologyBirthDateTree; }
  get numerologyPrint() { return this.configuration.baseUrl + this._numerologyPrint; }
  get getNumerologyPrint() { return this.configuration.baseUrl + this._getNumerologyPrint; }
  get deleteNumerologyPortrait() { return this.configuration.baseUrl + this._deleteNumerologyPrint; }

  constructor(private http: HttpClient, private configuration: ConfigurationService, private injector: Injector)
  {
    super();
  }

  /// We should use this
  getNamesEndpoint<T>(request: string): Observable<T> {
    let endpoint = this.nameCalculationUrl + request + "/";

    return this.http.get<T>(endpoint, this.getRequestHeaders()).pipe<T>(
      catchError(error => {
        return this.handleError(error);
      })
    );
  }

  getNamesWholeEndpoint<T>(baseNames: string, addedNames: string): Observable<T> {
    if(addedNames == "")
      addedNames = "-";

    let endpoint = this.nameCalculationWholeUrl + baseNames + "/" + addedNames + "/";
    return this.http.get<T>(endpoint, this.getRequestHeaders()).pipe<T>(
      catchError(error => {
        return this.handleError(error);
      })
    );
  }

  getNumerologyBirthDate<T>(request: string): Observable<T> {
    let endpoint = this.numerologyBirthDate + request + "/";

    return this.http.get<T>(endpoint, this.getRequestHeaders()).pipe<T>(
      catchError(error => {
        return this.handleError(error);
      })
    );
  }

  deleteNumerologyPortraitEndpoint<T>(request: number): Observable<T> {
    let endpoint = this.deleteNumerologyPortrait + request + "/";

    return this.http.delete<T>(endpoint, this.getRequestHeaders()).pipe<T>(
      catchError(error => {
        return this.handleError(error);
      })
    );
  }

  getPrintPortraitEndpoint<T>(path: string): Observable<T> {
    let endpoint = this.getNumerologyPrint + path + "/";

    return this.http.post<T>(endpoint, this.getRequestHeaders(), { responseType: 'blob' as 'json' , reportProgress: true }).pipe<T>(
      catchError(error => {
        debugger;
        return this.handleError(error);
      })
    );
  }

  getNumerologyBirthDateTree<T>(request: string): Observable<T> {
    let endpoint = this.numerologyBirthDateTree + request + "/";

    return this.http.get<T>(endpoint, this.getRequestHeaders()).pipe<T>(
      catchError(error => {
        return this.handleError(error);
      })
    );
  }

  addNewPortraitEndpoint<T>(request: NumerologyPortrait): Observable<T> {
    let endpoint = this.addNewPortraitUrl;

    return this.http.post<T>(endpoint, request, this.getRequestHeaders()).pipe<T>(
      catchError(error => {
        return this.handleError(error);
      })
    );
  }

  printPortraitEndpoint<T>(request: any): Observable<T> {
    let endpoint = this.numerologyPrint;

    return this.http.post<T>(endpoint, request, this.getRequestHeaders()).pipe<T>(
      catchError(error => {
        return this.handleError(error);
      })
    );
  }

  /// We should use this
  getClientPortraits<T>(id: number): Observable<T> {
    let endpoint = this.clientPortraitUrl + id;

    return this.http.get<T>(endpoint, this.getRequestHeaders()).pipe<T>(
      catchError(error => {
        return this.handleError(error);
      })
    );
  }
}