using N1;

namespace Figuras{
	public abstract class Circulo : Figura, MismoTypes{
		public int field1;
		public string field2;
		public static float field3;
		public bool field4;
		private Types t;
		myClase[,,][][] field5 = new myClase[3,2][][]{{5,2}, {2,3}, {4,5}};

		public override void methodo(){}
		public override void methodo2(){}
		public override string virtualmethodo();

		public virtual void MismoMethodo(){}
		public abstract void MismoMethodo2();

	}
	namespace insideFiguras{
		public interface Types: myInterface{
			
		}
	}
	public interface MismoTypes: myInterface,MismoNms, outsideInterface{
			void MismoMethodo();
			void MismoMethodo2();
	}
}
public interface myInterface : Types{
	void getNombre(int index);
	void getNompre(string name);
	void getNombre(int[] index);
	void getNombre(int[,,][] index);
	int setIndex(int index);
}

public interface Types{
			
}