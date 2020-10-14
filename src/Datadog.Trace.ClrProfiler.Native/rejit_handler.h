#ifndef DD_CLR_PROFILER_REJIT_HANDLER_H_
#define DD_CLR_PROFILER_REJIT_HANDLER_H_

#include <atomic>
#include <mutex>
#include <string>
#include <unordered_map>
#include <vector>

#include "cor.h"
#include "corprof.h"
#include "logging.h"
#include "module_metadata.h"

namespace trace {

class RejitHandlerModuleMethod {
 private:
  mdMethodDef methodDef;
  ICorProfilerFunctionControl* pFunctionControl;
  FunctionInfo* functionInfo;
  MethodReplacement* methodReplacement;
  std::mutex functionsIds_lock;
  std::unordered_set<FunctionID> functionsIds;
  void* module;

 public:
  RejitHandlerModuleMethod(mdMethodDef methodDef, void* module) {
    this->methodDef = methodDef;
    this->pFunctionControl = nullptr;
    this->module = module;
  }
  inline mdMethodDef GetMethodDef() { return this->methodDef; }
  inline ICorProfilerFunctionControl* GetFunctionControl() {
    return this->pFunctionControl;
  }
  inline void SetFunctionControl(
      ICorProfilerFunctionControl* pFunctionControl) {
    this->pFunctionControl = pFunctionControl;
  }
  inline FunctionInfo* GetFunctionInfo() { return this->functionInfo; }
  inline void SetFunctionInfo(FunctionInfo* functionInfo) {
    this->functionInfo = functionInfo;
  }
  inline MethodReplacement* GetMethodReplacement() {
    return this->methodReplacement;
  }
  inline void SetMethodReplacement(MethodReplacement* methodReplacement) {
    this->methodReplacement = methodReplacement;
  }
  inline void* GetModule() { return this->module; }
  void AddFunctionId(FunctionID functionId);
  bool ExistFunctionId(FunctionID functionId);
  void Dump();
};

class RejitHandlerModule {
 private:
  ModuleID moduleId;
  ModuleMetadata* metadata;
  std::mutex methods_lock;
  std::unordered_map<mdMethodDef, RejitHandlerModuleMethod*> methods;
  void* handler;

 public:
  RejitHandlerModule(ModuleID moduleId, void* handler) {
    this->moduleId = moduleId;
    this->metadata = nullptr;
    this->handler = handler;
  }
  inline ModuleID GetModuleId() { return this->moduleId; }
  inline ModuleMetadata* GetModuleMetadata() { return this->metadata; }
  inline void SetModuleMetadata(ModuleMetadata* metadata) {
    this->metadata = metadata;
  }
  inline void* GetHandler() { return this->handler; }
  RejitHandlerModuleMethod* GetOrAddMethod(mdMethodDef methodDef);
  void Dump();
};

class RejitHandler {
 private:
  std::mutex modules_lock;
  std::unordered_map<ModuleID, RejitHandlerModule*> modules;
  std::mutex methodByFunctionId_lock;
  std::unordered_map<FunctionID, RejitHandlerModuleMethod*> methodByFunctionId;
  ICorProfilerInfo4* profilerInfo;

  RejitHandlerModuleMethod* GetModuleMethodFromFunctionId(
      FunctionID functionId);

 public:
  RejitHandler(ICorProfilerInfo4* pInfo) { profilerInfo = pInfo; }
  RejitHandlerModule* GetOrAddModule(ModuleID moduleId);

  void NotifyReJITParameters(ModuleID moduleId, mdMethodDef methodId,
                          ICorProfilerFunctionControl* pFunctionControl,
                          ModuleMetadata* metadata);
  void NotifyReJITCompilationStarted(FunctionID functionId, ReJITID rejitId);
  void Dump();
  void _addFunctionToSet(FunctionID functionId,
                         RejitHandlerModuleMethod* method);
};

}  // namespace trace

#endif  // DD_CLR_PROFILER_REJIT_HANDLER_H_