#ifndef MODEL_CLASS_H
#define MODEL_CLASS_H

#include <vector>

#include "BaseDataClass.h"

typedef struct {

	uint32_t length = 0;
	std::vector<uint8_t> data;

} POF0;

struct nmdmStruct {

	uint32_t chunk_body_size = 0;
	std::vector<bool> chunk_body;
	POF0 chunk_pointer;

};

typedef struct {

	float x;
	float y;
	float z;

} NJS_POINT3, NJS_VECTOR;

typedef union {

	uint32_t color;
	struct {
		int16_t u, v;
	} tex;
	struct {
		int8_t b, g, r, a;
	} argb;

} NJS_COLOR;

typedef struct {

	NJS_COLOR diffuse;
	NJS_COLOR specular;
	float exponent;
	uint32_t attr_tex_id;
	uint32_t attr_flags;

} NJS_MATERIAL;

typedef struct {

	uint16_t type_mat_id;		//	type and material id		//	upper 2 bits say what type: triangular polygons, quadrilateral polygons, N-sided polygons, continiguous polygons(trimesh)
	uint16_t numMesh;			//	number of polygons
	int16_t* meshes;			//	polygon list
	uint32_t* attrs;			//	polygon attributes
	NJS_VECTOR* normals;		//	polygon normal line list
	NJS_COLOR* vert_color;		//	vector color list
	NJS_COLOR* vertuv;			//	vector UV list

} NJS_MESHSET;

typedef struct {

	NJS_POINT3* points;			//	vertex list
	NJS_VECTOR* normals;		//	normal line list
	uint32_t numPoints;			//	number of points
	NJS_MESHSET* meshsets;		//	polygon list
	NJS_MATERIAL* mats;			//	material lists
	uint16_t numMeshsets;		//	number of mesh lists
	uint16_t numMats;			//	number of material lists
	NJS_POINT3 center;			//	model center
	float r;					//	radius of circumscribed sphere

} NJS_MODEL;					//	NJCM model

typedef struct {

	uint32_t eval_flags;		//	evaluation method optimization
	NJS_MODEL* model;
	float pos[3];				//	parallel motion
	float ang[3];				//	rotation
	float scl[3];				//	scale
	NJS_OBJECT* child;			//	child object
	NJS_OBJECT* sibling;		//	sibling object

} NJS_OBJECT;

typedef struct {

	std::vector<uint32_t> magicTex;						//	super magical *.dds offset 0x20 number used to link texture to model

} GIXL_CHUNK;

typedef struct {

	uint32_t length = 0;
	uint16_t nbSizeOfIntChunks = 0;
	uint16_t unknown = 0;
	std::vector<uint32_t> data;

} MCS_;

typedef struct {

	size_t chunk_body_hash = 0;

	GIXL_CHUNK textureMagic;
	uint32_t chunk_body_size = 0;
	std::vector<bool> chunk_body;
	POF0 chunk_pointer;
	MCS_* chunk_mcs = nullptr;

	bool mixlRead = false;
	std::vector<nmdmStruct> motions;

	std::string filename = "";

} MODEL_OBJECT;

class ModelClass : public BaseDataClass {

public:
	ModelClass() {};
	void write();
	void read(std::string);
	void draw();
	void outputToCSV();

private:

};

#endif
