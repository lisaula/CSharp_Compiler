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
    ToString() {}
}
GlobalNamespace.IntType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {}
    static ParseStringType(s) {}
}
GlobalNamespace.CharType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {}
    static ParseStringType(s) {}
}
GlobalNamespace.DictionaryTypeNode = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {}
}
GlobalNamespace.FloatType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {}
    static ParseStringType(s) {}
}
GlobalNamespace.StringType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {}
}
GlobalNamespace.VarType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {}
}
GlobalNamespace.VoidTypeNode = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
}
GlobalNamespace.BoolType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {}
}
GlobalNamespace.System.Console = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    static WriteLineStringType(message) {}
    static ReadLine() {}
}
GlobalNamespace.System.Console.Out = null;
GlobalNamespace.System.Console.In = null;
GlobalNamespace.System.IO.TextWriter = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    static WriteLineStringType(message) {}
}
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