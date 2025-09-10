import { inject, Injectable, signal } from '@angular/core';
import { Food } from '../Models/Food.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FoodService {

  private http = inject(HttpClient)
  private baseUrl = 'https://localhost:7122/api'

  getFoods():Observable<Food[]>{
    return this.http.get<Food[]>(`${this.baseUrl}/Foods`)
  }
  // foods = signal<Food[]>([
  //   {id:1,name:"Dose",price:140,category:"Breakfast",imageUrl:"dosa.jpeg"},
  //   {id:2,name:"Chapathi",price:120,category:"Lunch",imageUrl:"chapathi.JPG"},
  //   {id:3,name:"Idli",price:90,category:"Breakfast",imageUrl:"idli.jpg"}
  // ])

  // selectedfood = signal<Food|null>(null)
  // cart = signal<Food[]>([]) 
  // constructor(){}
  // OnFoodSelected(food:Food){
  //   this.selectedfood.set(food)
  // }

  // addtocart(food:Food){
  //   this.cart.update(cur => [...cur,food])
  // }

  // clearCart(food:Food){
  //   this.cart.set([])
  // }

  // removeCart(Id:number){
  //   this.cart.update((curr)=>curr.filter((item)=>item.id !== Id))
  // }
}
