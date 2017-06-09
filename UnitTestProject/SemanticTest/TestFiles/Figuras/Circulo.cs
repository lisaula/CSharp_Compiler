using N1;

public interface Types{
			
}
namespace Figuras{
	public class Circulo : Figura{

	}
	namespace insideFiguras{
		public interface Types{
			
		}
	}
}
public interface myInterface : Types, Types{
	void getNombre(int index);
	void getNompre(string name);
	int setIndex(int index);
}