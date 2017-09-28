//authHttpServiceFactory class is completed by Nolawe Woldesemayat and the http wrapping function
//is gathered from auth0 website tutorial.
import {AuthConfig, AuthHttp} from "angular2-jwt";
import {Http, RequestOptions} from "@angular/http/http";
import {AuthService} from "./auth/auth.service";
/**
 * Created by Owner on 9/24/2017.
 */
export function authHttpServiceFactory(http: Http, options: RequestOptions, authService:AuthService) {
  return new AuthHttp(new AuthConfig({
    tokenName: 'token',
    tokenGetter: (() => localStorage.getItem('id_token')),
    globalHeaders: [{'Content-Type': 'application/json'}],
  }), http, options);
}
