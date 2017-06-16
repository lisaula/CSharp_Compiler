using Figuras;

namespace N1{

	public class myClase: Circulo{
		static Circulo c = new Circulo();
		static myClase clase = c as Circulo;
		
		public static int ca = (int)'a';
		int cat = ca;
		private static bool b = true;
		static bool f = false;
		bool r = b && f;
		float f1 = 1.2f;
		int i1 = 50;
		float f2 = ((float)1.5);
		bool r2 = c == null;
		bool h = clase is Circulo;
		public myClase(){
		}
		public void metodo(){
			(count)++;
			((parent)child).print();
		}

		public void getType(int index, bool maybe){

		}
		public myClase(int nuevo){
			var kaka = 5;
			int n = nuevo;
			float mama = 5000f;
			Circulo circulo = c?? null;
			metodo();
			do{
				kaka--;
				int kakos = kaka;
				int[] array = {1,2,3,4};
				foreach(var nueva in array){
					for(int i = 0; i < 4;i++){
						if(n < 10){

						} else if(array[5]<0){

						}
					}
				}
			}while(kaka >0);
		}
		public void getType(Circulo index, string t){

		}
	}

	public interface outsideInterface{

	}
	namespace N2{
		public class myClase2{

		}	
		
	}
}

public class myClaseAlone{

}