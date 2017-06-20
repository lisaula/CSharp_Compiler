let GlobalNamespace = {};
GlobalNamespace.System = {};
GlobalNamespace.System.IO = {};
GlobalNamespace.RaimProgram.Common = {};
GlobalNamespace.RaimProgram.Common.Sorting = {};
GlobalNamespace.RaimProgram.Base = {};
GlobalNamespace.RaimProgram.Base.Derivatives = {};
GlobalNamespace.RaimProgram.Base.Derivatives = {};
GlobalNamespace.RaimProgram = {};
GlobalNamespace.Object = class {
    Object() {}
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {
        console.log(this);
    }
}
GlobalNamespace.IntType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {
        console.log(this);
    }
    static ParseStringType(s) {
        return +(s);
    }
}
GlobalNamespace.CharType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {
        console.log(this);
    }
    static ParseStringType(s) {
        return s;
    }
}
GlobalNamespace.FloatType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {
        console.log(this);
    }
    static ParseStringType(s) {
        return +(s);
    }
}
GlobalNamespace.StringType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {
        console.log(this);
    }
}
GlobalNamespace.BoolType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {
        console.log(this);
    }
}
GlobalNamespace.System.Console = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    static WriteLineStringType(message) {
        console.log(message);
     }
    static ReadLine() {

    }
}
GlobalNamespace.System.IO.TextWriter = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    static WriteLineStringType(message) {
        console.log(message);
    }
}
GlobalNamespace.System.Console.Out = new GlobalNamespace.System.IO.TextWriter();
GlobalNamespace.System.IO.TextReader = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    static ReadLine() {}
}
GlobalNamespace.RaimProgram.Common.StudentType =  {
 FRESHMAN : 0,
 SOPHOMORE : 1,
 JUNIOR : 2,
 SENIOR : 3,
}
GlobalNamespace.RaimProgram.Base.Person = class extends GlobalNamespace.Object {
Person() {
}
PersonStringTypeIntType(name,age) {
this.Name = name;
this.Age = age;
}
constructor(){
super()
this.Name = null;
this.Age = null;
let argumentos = Array.from(arguments);
let argus = argumentos.slice(1);
if(argumentos.length>1)
	this[arguments[0]](...argus);
}
PrintClassTypeWithNumberCall() {
let numberCall = this.GetAndIncrementNumberCall();
GlobalNamespace.System.Console.WriteLineStringType((("Person") + (" ")) + itoa(numberCall));
}
GetAndIncrementNumberCall() {
_numberCall++;
return this._numberCall;
}
SortPersonsPersonArray1DIntType(persons,size) {
}
}
GlobalNamespace.RaimProgram.Base.Person._numberCall = 0;
GlobalNamespace.RaimProgram.Base.Derivatives.Student = class extends GlobalNamespace.RaimProgram.Base.Person {
Student() {

}
StudentStringTypeIntType(name,age) {

this.Name = this.name;
this.Age = this.age;
}
constructor(){
super()
this.studentType = null;
let argumentos = Array.from(arguments);
let argus = argumentos.slice(1);
if(argumentos.length>1)
	this[arguments[0]](...argus);
}
SetStudentTypeStudentType(type) {
GlobalNamespace.RaimProgram.Common.StudentType = GlobalNamespace.RaimProgram.Common.type;
}
SetStudentTypeIntType(type) {
switch ( type){
case (0): 
this.studentType = GlobalNamespace.RaimProgram.Common.StudentType.FRESHMAN
break 
case (1): 
this.studentType = GlobalNamespace.RaimProgram.Common.StudentType.SOPHOMORE
break 
case (2): 
this.studentType = GlobalNamespace.RaimProgram.Common.StudentType.JUNIOR
break 
case (3): 
this.studentType = GlobalNamespace.RaimProgram.Common.StudentType.SENIOR
break 
default :
GlobalNamespace.System.Console.WriteLineStringType("Invalid entry for StudentType")
break 
};
}
GetStudentType() {
return GlobalNamespace.RaimProgram.Common.StudentType;
}
SortPersonsPersonArray1DIntType(persons,size) {
this.QuickSortPersonArray1DIntTypeIntType(persons,0,(size) - (1));
}
ToString() {
return ((("Name: ") + (this.Name)) + ("\nAge: ")) + itoa(this.Age);
}
QuickSortPersonArray1DIntTypeIntType(persons,left,right) {
let i = left;
let j = right;
let pivot = persons[(((left) + (right))) / (2)];
do {
while((((persons[i].Age) < (pivot.Age))) && (((i) < (right)))){
i++
};
while((((pivot.Age) < (persons[j].Age))) && (((j) > (left)))){
j--
};
if( (i) <= (j) ){
let temp = persons[i];
persons[i] = persons[j];
persons[j] = temp;
i++;
j--;
};
} while ((i) <= (j)
);;
if( (left) < (j) ){
this.QuickSortPersonArray1DIntTypeIntType(persons,left,j)
};
if( (i) < (right) ){
this.QuickSortPersonArray1DIntTypeIntType(persons,i,right)
};
}
}
GlobalNamespace.RaimProgram.Base.Derivatives.Random = class extends GlobalNamespace.Object {
Random() {

}
constructor(){
super()
let argumentos = Array.from(arguments);
let argus = argumentos.slice(1);
if(argumentos.length>1)
	this[arguments[0]](...argus);
}
Nextfloat() {
return 5.5;
}
}
GlobalNamespace.RaimProgram.Base.Derivatives.Teacher = class extends GlobalNamespace.RaimProgram.Base.Person {
Teacher() {

this.random = new GlobalNamespace.RaimProgram.Base.Derivatives.Random("Random");
}
TeacherStringTypeIntType(name,age) {

this.Name = this.name;
this.Age = this.age;
this.random = new GlobalNamespace.RaimProgram.Base.Derivatives.Random("Random");
}
constructor(){
super()
let argumentos = Array.from(arguments);
let argus = argumentos.slice(1);
if(argumentos.length>1)
	this[arguments[0]](...argus);
}
GetRandomGrumpinessFloatTypeFloatType(lowerLimit,upperLimit) {
let range = (upperLimit) - (lowerLimit);
let number = this.random.Nextfloat();
return ((((number) * (range)))) + (lowerLimit);
}
SortPersonsPersonArray1DIntType(persons,size) {
this.BubbleSortPersonArray1DIntType(persons,size);
}
ToString() {
return ((("Name: ") + (this.Name)) + ("\nAge: ")) + itoa(this.Age);
}
static BubbleSortPersonArray1DIntType(persons,size) {
for(let pass = 1;(pass) < (size);
pass++) {
for(let i = 0;(i) < ((size) - (pass));
i++) {
if( (persons[i].Age) >= (persons[(i) + (1)].Age) ){
let temp = persons[i];
persons[i] = persons[(i) + (1)];
persons[(i) + (1)] = temp;
};
};
};
}
}
GlobalNamespace.RaimProgram.Base.Derivatives.Teacher.random = null;
GlobalNamespace.RaimProgram.Program = class extends GlobalNamespace.Object {
Program() {

}
constructor(){
super()
let argumentos = Array.from(arguments);
let argus = argumentos.slice(1);
if(argumentos.length>1)
	this[arguments[0]](...argus);
}
static MainStringTypeArray1D(args) {
let student = new GlobalNamespace.RaimProgram.Base.Derivatives.Student("Student");
let students = [];
students[0] = new GlobalNamespace.RaimProgram.Base.Derivatives.Student("StudentStringTypeIntType","D",22);
students[1] = new GlobalNamespace.RaimProgram.Base.Derivatives.Student("StudentStringTypeIntType","C",22);
students[2] = new GlobalNamespace.RaimProgram.Base.Derivatives.Student("StudentStringTypeIntType","B",40);
students[3] = new GlobalNamespace.RaimProgram.Base.Derivatives.Student("StudentStringTypeIntType","A",35);
student.SortPersonsPersonArray1DIntType(students,4);
this.PrintPersonsInfoPersonArray1D(students);
GlobalNamespace.System.Console.WriteLineStringType("");
let teacher = new GlobalNamespace.RaimProgram.Base.Derivatives.Teacher("Teacher");
let teachers = [];
teachers[0] = new GlobalNamespace.RaimProgram.Base.Derivatives.Teacher("TeacherStringTypeIntType","Za",22);
teachers[1] = new GlobalNamespace.RaimProgram.Base.Derivatives.Teacher("TeacherStringTypeIntType","Yb",22);
teachers[2] = new GlobalNamespace.RaimProgram.Base.Derivatives.Teacher("TeacherStringTypeIntType","Xc",40);
teachers[3] = new GlobalNamespace.RaimProgram.Base.Derivatives.Teacher("TeacherStringTypeIntType","Wd",35);
teacher.SortPersonsPersonArray1DIntType(teachers,4);
this.PrintPersonsInfoPersonArray1D(teachers);
}
static PrintPersonsInfoPersonArray1D(persons) {
for(let p of persons){
if( p instanceof GlobalNamespace.RaimProgram.Base.Derivatives.Teacher ){
let t = p;
GlobalNamespace.System.Console.WriteLineStringType((t.Name) + (" is a teacher."));
GlobalNamespace.System.Console.WriteLineStringType(t.ToString());
t.PrintClassTypeWithNumberCall();
GlobalNamespace.System.Console.WriteLineStringType((("Grumpiness: ") + itoa(t.GetRandomGrumpinessFloatTypeFloatType(1,100))) + ("\n"));
}
else {
let s = p;
GlobalNamespace.System.Console.WriteLineStringType((s.Name) + (" is not a teacher."));
s.PrintClassTypeWithNumberCall();
};
};
}
}
module.exports = GlobalNamespace;