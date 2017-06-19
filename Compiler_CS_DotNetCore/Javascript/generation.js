let GlobalNamespace = {};
GlobalNamespace.System = {};
GlobalNamespace.System.IO = {};
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
    static ReadLine() {}
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
GlobalNamespace.sort = class {
    sort() {}
    constructor() {
        this.prueba = 5;
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    static IntArrayMinIntTypeArray1DIntTypeIntType(data, start, size) {
        let minPos = start;
        for (let pos = (start) + (1);
            (pos) < (size); pos++) {
            if ((data[pos]) < (data[minPos])) {
                minPos = pos
            }
        };
        return minPos;
    }
    static IntArraySelectionSortIntTypeArray1DIntType(data, size) {
        let i = null;
        let N = size;
        GlobalNamespace.System.Console.WriteLineStringType("Hola");
        let n = GlobalNamespace.IntType.ParseStringType("5");
        for (i = 0;
            (i) < ((N) - (1)); i++) {
            let k = this.IntArrayMinIntTypeArray1DIntTypeIntType(data, i, size);
            if ((i) != (k)) {
                this.exchangeIntTypeArray1DIntTypeIntType(data, i, k)
            };
        };
    }
    static exchangeIntTypeArray1DIntTypeIntType(data, m, n) {
        let temporary = null;
        temporary = data[m];
        data[m] = data[n];
        data[n] = temporary;
    }
}
module.exports = GlobalNamespace;