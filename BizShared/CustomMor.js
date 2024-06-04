"use strict";
// [Stroke] Structure
Object.defineProperty(exports, "__esModule", { value: true });
exports.bin__FactBroadcast = exports.FactBroadcast__bin = exports.bin__FactWhiteboard = exports.FactWhiteboard__bin = exports.bin__ActionWhiteboard = exports.ActionWhiteboard__bin = exports.bin__Stroke = exports.Stroke__bin = void 0;
var Stroke__bin = function (bb) { return function (v) {
    List__bin((function (bb) { return function (v) {
        var v0 = v.v0;
        float32__bin(bb)(v0);
        var v1 = v.v1;
        float32__bin(bb)(v1);
    }; }))(bb)(v.points);
    float32__bin(bb)(v.strokeSize);
    str__bin(bb)(v.color);
}; };
exports.Stroke__bin = Stroke__bin;
var bin__Stroke = function (bi) {
    return {
        points: bin__List((function (bi) {
            var v0 = bin__float32(bi);
            var v1 = bin__float32(bi);
            return { v0: v0, v1: v1 };
        }))(bi),
        strokeSize: bin__float32(bi),
        color: bin__str(bi),
    };
};
exports.bin__Stroke = bin__Stroke;
// [ActionWhiteboard] Structure
var ActionWhiteboard__bin = function (bb) { return function (v) {
    int32__bin(bb)(v.e);
    switch (v.e) {
        case 0:
            (0, exports.Stroke__bin)(bb)(v);
            break;
        case 1:
            uint32__bin(bb)(v);
            break;
        case 2:
            str__bin(bb)(v);
            break;
    }
}; };
exports.ActionWhiteboard__bin = ActionWhiteboard__bin;
var bin__ActionWhiteboard = function (bi) {
    var v = {};
    v.e = bin__int32(bi);
    switch (v.e) {
        case 2:
            v.val = bin__str(bi);
            break;
        case 1:
            v.val = bin__uint32(bi);
            break;
        case 0:
            v.val = (0, exports.bin__Stroke)(bi);
            break;
    }
    return v;
};
exports.bin__ActionWhiteboard = bin__ActionWhiteboard;
// [FactWhiteboard] Structure
var FactWhiteboard__bin = function (bb) { return function (v) {
    (0, exports.ActionWhiteboard__bin)(bb)(v.action);
    str__bin(bb)(v.actor);
    int64__bin(bb)(v.clientId);
    int64__bin(bb)(v.serverId);
    DateTime__bin(bb)(v.clientTimestamp);
    DateTime__bin(bb)(v.serverTimestamp);
}; };
exports.FactWhiteboard__bin = FactWhiteboard__bin;
var bin__FactWhiteboard = function (bi) {
    return {
        action: (0, exports.bin__ActionWhiteboard)(bi),
        actor: bin__str(bi),
        clientId: bin__int64(bi),
        serverId: bin__int64(bi),
        clientTimestamp: bin__DateTime(bi),
        serverTimestamp: bin__DateTime(bi),
    };
};
exports.bin__FactWhiteboard = bin__FactWhiteboard;
// [FactBroadcast] Structure
var FactBroadcast__bin = function (bb) { return function (v) {
    int32__bin(bb)(v.e);
    switch (v.e) {
        case 0:
            (0, exports.FactWhiteboard__bin)(bb)(v);
            break;
        case 1:
            break;
    }
}; };
exports.FactBroadcast__bin = FactBroadcast__bin;
var bin__FactBroadcast = function (bi) {
    var v = {};
    v.e = bin__int32(bi);
    switch (v.e) {
        case 1:
            break;
        case 0:
            v.val = (0, exports.bin__FactWhiteboard)(bi);
            break;
    }
    return v;
};
exports.bin__FactBroadcast = bin__FactBroadcast;
