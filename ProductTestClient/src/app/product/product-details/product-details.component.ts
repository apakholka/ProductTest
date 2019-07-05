import { Component, OnInit } from '@angular/core';
import { ReactiveFormsBaseClass } from 'src/app/classes/reactive-forms-base-class';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductService } from 'src/app/services/product.service';
import { Product } from 'src/app/models/product-module';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ServerResponse } from 'src/app/models/server-responce-model';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent extends ReactiveFormsBaseClass implements OnInit {

  productForm:FormGroup;
  product:Product;
  offText='No';
  onText='Yes';
  state_default = true;

  constructor(private formBuilder: FormBuilder, private productService: ProductService, private router: Router,private toastr: ToastrService) {
    super(
      {
        id: null,
        name: '',
        category: '',
        active: false,
        price: null
      },
      {
        name: {
          required:  'Name is required.',
          minlength: 'Name must contain at least 2 symbols',
          maxlength: 'Name must contain max 15 symbols'
        },
        category: {
          required:  'Category is required.',
          minlength: 'Category must contain at least 2 symbols',
          maxlength: 'Category must contain max 15 symbols'
        },
        price: {
          required: 'Price is required.',
        },
      });
   }

  ngOnInit() {
    this.product =  this.productService.getProduct();
    console.log("this.product",this.product);
    if(this.product === undefined || this.product === null || Object.keys(this.product).length === 0){
      this.router.navigate(["/product_table"]);
    }
    else{ 
      this.createProductForm();
      this.productForm.controls.id.setValue(this.product.Id);
      this.productForm.controls.name.setValue(this.product.Name);
      this.productForm.controls.category.setValue(this.product.Category);
      this.productForm.controls.active.setValue(this.product.Active);
      this.productForm.controls.price.setValue(this.product.Price);
    }
  }

  private createProductForm(): void {
    this.productForm = this.formBuilder.group({
      id: [null],
      name: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(15)]],
      category: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(15)]],
      active: [false],
      price: [null,[Validators.required]]
    });
    this.productForm.valueChanges.subscribe(data => this.onValueChanged(this.productForm, data));
    this.onValueChanged(this.productForm);
  }

  editProduct(){
    this.productService.updateProduct(this.productForm.value).subscribe((data:ServerResponse)=>{
      this.router.navigate(["/product_table"]);
      this.toastr.success(data.message);
    })
  }

}
