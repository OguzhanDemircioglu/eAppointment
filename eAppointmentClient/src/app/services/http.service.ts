import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {ResultModel} from "../models/result.model";
import {api} from "../constats";

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  token: string = "";
  constructor(private http: HttpClient) {
    if(localStorage.getItem("token")){
      this.token = localStorage.getItem("token") ?? "";
    }
  }

  post<T>(apiUrl: string, body: any, callBack: (res: ResultModel<T>) => void, errCallBack?: (err: HttpErrorResponse) => void) {
    this.http.post<ResultModel<T>>(`${api}/${apiUrl}`,body, {
      headers: {
        "Authorization": "Bearer " + this.token
      }
    })
      .subscribe({
      next: (res => {
        callBack(res);
      }),
      error: (err: HttpErrorResponse) => {
        if (errCallBack !== undefined) {
          errCallBack(err);
        }
      }
    })
  }

}
