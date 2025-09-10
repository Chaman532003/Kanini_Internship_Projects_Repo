export interface Food{
    id:number
    name:string
    price:number
    category:string
    imageUrl:string
}

export interface CartItem{
    id:number
    FoodId:number
    Quantity:number
    Food:Food
}

