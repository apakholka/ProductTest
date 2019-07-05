import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Product } from '../models/product-module';


@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private API_URL= environment.API_URL;
  private product: Product = new Product();

  constructor(private http: HttpClient) { }

    getProduct(): Product {
      return this.product;
   }

  setProduct(data: Product) {
      this.product = data;
   }   

  getProductsList(sortField:string, sortBy:string){
    let params = new HttpParams()
    if(sortField != null){
      params = params.append("sortField",sortField)
    }
    if(sortBy != null){
      params = params.append("sortBy",sortBy)
    }
    return this.http.get(this.API_URL + 'api/product',{params:params});
  }

  updateProduct(product: Product){
    return this.http.put(this.API_URL + 'api/product', product);
  }

  getProductDetails(productId:number){
    return this.http.get(this.API_URL + 'api/product/' + productId);
  }
}
