import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
  })

export class DataService {

    constructor() { }

    getFormattedParams(params: any): any {
        let result: any;

        if (Array.isArray(params)) {
            if (params.length > 0 && typeof (params[0]) === 'number')
                return JSON.stringify(params);

            result = [];
            params.forEach(entity => {
                result.push(this.getFormattedParams(entity));
            });
            return JSON.stringify(result);
        }
        result = {};
        for (let key in params) {
            if (params.hasOwnProperty(key)) {
                let val = params[key];
                if (val !== undefined && val !== null) {
                    //Handle the Date javascript object             
                    if (val instanceof Date) {
                        val = `${DataService.Format2Digits(val.getFullYear())}-${DataService.Format2Digits(val.getMonth() + 1)}-${DataService.Format2Digits(val.getDate())}T00:00:00.000Z`;
                    }
                    result[key] = val;
                }
            }
        }
        return result;
    }

    public static Format2Digits(n: any) {
        return n > 9 ? "" + n : "0" + n;
    }

}