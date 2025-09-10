import { Component, inject } from '@angular/core';
import { FoodService } from '../food.service';
import { Food } from '../../Models/Food.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-food-list',
  imports: [],
  templateUrl: './food-list.html',
  styleUrl: './food-list.css'
})
export class FoodList {

  private foodservice = inject(FoodService)
  private router = inject(Router)
  foods:Food[] = []

  ngOnInit(){
    this.foodservice.getFoods().subscribe(data => this.foods = data)
  }
  // foodservice = inject(FoodService)
  
  // viewDetails(food:Food){
  //   this.foodservice.OnFoodSelected(food)
  // }

  // addFood(food:Food){
  //   this.foodservice.addtocart(food)
  // }

  // clearCart(food:Food){
  //   this.foodservice.clearCart(food)
  // }

  // removeCart(food:Food){
  //   this.foodservice.removeCart(food.id)
  // }

}
