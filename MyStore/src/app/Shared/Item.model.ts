// export class Item
// {
//     constructor(public code:string, public name:string,public price:number,public amount:number,public imageURL:string,public discription:string){}
// }
type Item  = {
    id: number,
    code:string,
    name:string,
    price:number,
    amount:number,
    imageURL:string,
    discription:string
}

export default Item;