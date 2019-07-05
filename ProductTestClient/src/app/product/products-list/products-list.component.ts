import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models/product-module';
import { ProductService } from 'src/app/services/product.service';
import { Router } from '@angular/router';
import { ServerResponse } from 'src/app/models/server-responce-model';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.scss']
})
export class ProductsListComponent implements OnInit {

productList:Array<Product>;
tableHeaders = ['Id','Name','Category','Active','Price'];
sortBy = 'desc';

  constructor(private productService: ProductService, private router:Router) {}

  ngOnInit() {
    this.productService.getProductsList(null, null).subscribe((data:any)=>{
      this.productList = data.result;
    });
  }

  sortColumn(sortKey:string){
    if(this.sortBy === 'desc'){
      this.sortBy = 'asc';
     }
     else{
      this.sortBy = 'desc'
     }
      this.productService.getProductsList(sortKey, this.sortBy).subscribe((data:ServerResponse)=>{
        this.productList = data.result;
      });
   }

   productDetails(productId:number){
     this.productService.getProductDetails(productId).subscribe((data:ServerResponse)=>{
       this.productService.setProduct(data.result);
       this.router.navigate(["/product_details"]);
     })
   }
}
