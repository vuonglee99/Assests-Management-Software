import { ViewEncapsulation, Component, Injector, OnInit ,Input , Output,EventEmitter} from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";

@Component({
  selector: 'app-page-list-user',
  templateUrl: './page-list-user.component.html',
  styleUrls: ['./page-list-user.component.css']
})
export class PageListUserComponent  extends AppComponentBase implements OnInit {

  @Output('nameUser') senUser = new EventEmitter<string>();

  constructor(injector: Injector) {
    super(injector);
  }

 ngOnInit(): void {

  }

  nameUser;
  filterInput;
  listSelectRow;
  records = [{"stt":1,"name":"Volkman, Witting and Waelchi","tel":"550-539-3438"},
  {"stt":2,"name":"Reilly, Schimmel and Frami","tel":"891-706-9556"},
  {"stt":3,"name":"Lowe and Sons","tel":"380-950-4379"},
  {"stt":4,"name":"Kiehn, Pouros and Leffler","tel":"618-885-1255"},
  {"stt":5,"name":"Larkin, Nolan and Hilll","tel":"579-207-7935"},
  {"stt":6,"name":"Jacobi, Wilderman and Medhurst","tel":"678-640-6580"},
  {"stt":7,"name":"Krajcik-Feeney","tel":"702-214-4525"},
  {"stt":8,"name":"Cassin-Howell","tel":"225-565-7700"},
  {"stt":9,"name":"Hammes Inc","tel":"615-366-0557"},
  {"stt":10,"name":"Wiza-Abernathy","tel":"234-845-8431"},
  {"stt":11,"name":"Altenwerth, Dicki and Gislason","tel":"230-106-8926"},
  {"stt":12,"name":"Bednar, Purdy and Denesik","tel":"613-104-0188"},
  {"stt":13,"name":"Bruen, Rice and Bauch","tel":"335-934-3432"},
  {"stt":14,"name":"Greenholt, Langworth and Harvey","tel":"535-563-6465"},
  {"stt":15,"name":"Jenkins, D'Amore and Terry","tel":"479-176-4084"},
  {"stt":16,"name":"Kautzer Inc","tel":"506-641-2507"},
  {"stt":17,"name":"Graham, Krajcik and Reichel","tel":"601-546-9171"},
  {"stt":18,"name":"Jakubowski-Fadel","tel":"512-121-0805"},
  {"stt":19,"name":"Schuster Group","tel":"245-500-6784"},
  {"stt":20,"name":"Runolfsdottir-Jacobi","tel":"301-271-9760"},
  {"stt":21,"name":"Kovacek LLC","tel":"290-867-5679"},
  {"stt":22,"name":"Kessler, Ziemann and Gusikowski","tel":"250-367-2228"},
  {"stt":23,"name":"Heaney, Pollich and Herzog","tel":"638-358-9908"},
  {"stt":24,"name":"Satterfield Inc","tel":"369-753-7604"},
  {"stt":25,"name":"Bogan-Goldner","tel":"314-382-9393"},
  {"stt":26,"name":"Hirthe-McLaughlin","tel":"500-114-1014"},
  {"stt":27,"name":"Flatley Inc","tel":"192-135-0959"},
  {"stt":28,"name":"Koepp-Keebler","tel":"240-787-2139"},
  {"stt":29,"name":"Yost Group","tel":"377-270-8282"},
  {"stt":30,"name":"Corwin-Lowe","tel":"298-693-4277"},
  {"stt":31,"name":"Reilly, Gutkowski and Mraz","tel":"842-352-9296"},
  {"stt":32,"name":"Kassulke, Borer and Lockman","tel":"360-151-1710"},
  {"stt":33,"name":"Pfannerstill-Reilly","tel":"225-397-9175"},
  {"stt":34,"name":"Bergnaum Inc","tel":"406-266-1671"},
  {"stt":35,"name":"Graham Inc","tel":"542-508-9287"},
  {"stt":36,"name":"Langosh-Bogan","tel":"314-450-6438"},
  {"stt":37,"name":"Hessel-Hackett","tel":"852-230-9615"},
  {"stt":38,"name":"Walker Inc","tel":"206-951-4493"},
  {"stt":39,"name":"Orn-Sporer","tel":"733-802-8551"},
  {"stt":40,"name":"Dicki-D'Amore","tel":"957-962-2146"},
  {"stt":41,"name":"Wiegand, Erdman and Botsford","tel":"886-339-7946"},
  {"stt":42,"name":"Smitham, Langosh and Kuvalis","tel":"973-493-6920"},
  {"stt":43,"name":"Huel-Ernser","tel":"200-445-4382"},
  {"stt":44,"name":"Zieme-Bernhard","tel":"303-674-7793"},
  {"stt":45,"name":"Wuckert, Reichel and Pfannerstill","tel":"771-296-3925"},
  {"stt":46,"name":"Strosin Group","tel":"170-285-9049"},
  {"stt":47,"name":"Leffler-Beer","tel":"230-681-3541"},
  {"stt":48,"name":"Schoen-Cummings","tel":"503-321-5566"},
  {"stt":49,"name":"Marquardt and Sons","tel":"192-600-9559"},
  {"stt":50,"name":"Ziemann-Greenholt","tel":"551-928-1164"}];
  cols = [
    { field: 'stt', header: 'STT' },
    { field: 'name', header: 'Tên phòng ban' },
    { field: 'tel', header: 'Số điện thoại' },
  ];

  viewDetail(){}
  search(){}

  selectedUser(){
    this.nameUser = this.listSelectRow.name;
    this.senUser.emit(this.nameUser);
  }
}
