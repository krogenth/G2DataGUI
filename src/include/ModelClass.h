#ifndef MODEL_CLASS_H
#define MODEL_CLASS_H

#include <vector>

#include "BaseDataClass.h"

struct POF0 {

	uint32_t length = 0;
	std::vector<uint8_t> data;

};

struct nmdmStruct {

	std::vector<uint8_t> chunk_body;
	POF0 chunk_pointer;

};

struct NJS_POINT3 {

	float x;
	float y;
	float z;

};

struct NJS_VECTOR {

	float x;
	float y;
	float z;

};

union NJS_COLOR {

	uint32_t color;
	struct tex {
		int16_t u, v;
	};
	struct argb {
		int8_t b, g, r, a;
	};
	
};

struct NJS_MATERIAL {

	NJS_COLOR diffuse;
	NJS_COLOR specular;
	float exponent;
	uint32_t attr_tex_id;
	uint32_t attr_flags;

};

struct NJS_MESHSET {

	uint16_t type_mat_id;				//	type and material id		//	upper 2 bits say what type: triangular polygons, quadrilateral polygons, N-sided polygons, continiguous polygons(trimesh)
	uint16_t numMesh;					//	number of polygons
	int16_t* meshes = nullptr;			//	polygon list
	uint32_t* attrs = nullptr;			//	polygon attributes
	NJS_VECTOR* normals = nullptr;		//	polygon normal line list
	NJS_COLOR* vert_color = nullptr;	//	vector color list
	NJS_COLOR* vertuv = nullptr;		//	vector UV list

};

struct NJS_MODEL {

	NJS_POINT3* points = nullptr;		//	vertex list
	NJS_VECTOR* normals = nullptr;		//	normal line list
	uint32_t numPoints;					//	number of points
	NJS_MESHSET* meshsets = nullptr;	//	polygon list
	NJS_MATERIAL* mats = nullptr;		//	material lists
	uint16_t numMeshsets;				//	number of mesh lists
	uint16_t numMats;					//	number of material lists
	NJS_POINT3 center;					//	model center
	float r;							//	radius of circumscribed sphere

};							//	NJCM model

struct NJS_OBJECT {

	uint32_t eval_flags;				//	evaluation method optimization
	NJS_MODEL* model;
	float pos[3];						//	parallel motion
	float ang[3];						//	rotation
	float scl[3];						//	scale
	NJS_OBJECT* child = nullptr;		//	child object
	NJS_OBJECT* sibling = nullptr;		//	sibling object

};

struct GIXL_CHUNK {

	std::vector<uint32_t> magicTex;						//	super magical *.dds offset 0x20 number used to link texture to model

};

struct MCS_ {

	uint32_t length = 0;
	uint16_t nbSizeOfIntChunks = 0;
	uint16_t unknown = 0;
	std::vector<uint32_t> data;

};

struct modelStruct {

	GIXL_CHUNK textureMagic;				//	GIXL can also tell use if the models are the same, based on their contained value
	NJS_OBJECT* model_object = nullptr;
	MCS_* chunk_mcs = nullptr;
	std::vector<nmdmStruct> motions;

	std::string filename = "";

};

class ModelClass : public BaseDataClass {

public:
	ModelClass() {};
	void write();
	void read(std::string);					//	reads an entire file that contains any number of models
	void draw();
	void outputToCSV();

private:
	std::vector<modelStruct> _models;

};

#endif
