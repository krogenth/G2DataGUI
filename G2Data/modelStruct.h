#pragma once
#include <vector>

#include "ImGui/imgui.h"

struct POF0 {

	ImU32 length = 0;
	std::vector<ImU8> data;

};

struct MCS_ {

	ImU32 length = 0;
	ImU32 nbEntries = 0;
	std::vector<ImU32> data;

};

enum EVAL_FLAGS {

	DO_NOT_APPLY_BY_POSITION    = 0b00000001,
	DO_NOT_APPLY_BY_ROTATION    = 0b00000010,
	DO_NOT_APPLY_BY_SCALE       = 0b00000100,
	DO_NOT_RENDER               = 0b00001000,
	BREAK_CHILD_TRACE           = 0b00010000,
	ZXY_ROTATION_BY_LIGHTWAVE3D = 0b00100000,
	SKIP_MOTION                 = 0b01000000,
	SKIP_SHAPE_MOTION           = 0b10000000

};

enum CHUNK_TYPE {

	BITS     = 0b00000001,
	TINY     = 0b00000010,
	MATERIAL = 0b00000100,
	VERTEX   = 0b00001000,
	STRIP    = 0b00010000

};

#pragma pack(1)
typedef struct {

	float x = 0.0f;
	float y = 0.0f;
	float z = 0.0f;

} NJS_POINT3, NJS_VECTOR;
#pragma pack()

#pragma pack(1)
typedef struct {

	ImU8 type = 0x29;
	ImU8 flag = 0;
	ImU16 nbIntChunks = 0;
	ImU32 unknown = 0;
	std::vector<NJS_VECTOR> vertex;

} NJS_VERTEX_LIST;
#pragma pack()

#pragma pack(1)
typedef struct {

	ImU8 chunk_head = 0;
	ImU8 chunk_flag = 0;

} NJS_INDICE_LIST_HEADER;
#pragma pack()

/*
ID 		Type 		Body Size
0 		Null 		0
1-5 	Bits 		0
8-9 	Tiny 		2
17-31 	Material 	2 + 2n
32-50 	Vertex 		2 + 4n
56-58 	Volume 		2 + 2n
64-75 	Strip 		2 + 2n
255 	End 		2 in plist, 4 in vlist
*/

#pragma pack(1)
typedef struct {



} NJS_BITS_CHUNK;
#pragma pack()

#pragma pack(1)
typedef struct {

	ImU16 chunk_body = 0;

} NJS_TINY_CHUNK;
#pragma pack()

#pragma pack(1)
typedef struct {

	/*
	use_diffuse = chunk.head & 0x01;
	use_specular = chunk.head & 0x02;
	use_ambient = chunk.head & 0x04;
	*/

	ImU16 chunk_length = 0;
	ImU32 diffuse_color = 0;
	ImU32 specular_color = 0;
	ImU32 ambient_color = 0;

} NJS_MATERIAL_CHUNK;
#pragma pack()

#pragma pack(1)
typedef struct {

	ImU16 chunk_length = 0;
	ImU16 vertex_offset = 0;
	ImU16 vertex_count = 0;
	std::vector<NJS_VECTOR> vertex;

} NJS_VERTEX_CHUNK;
#pragma pack()

#pragma pack(1)
typedef struct {

	ImU16 chunk_length = 0;
	ImU16 strip_count = 0;
	std::vector<ImU8> strip_data;

} NJS_STRIP_CHUNK;
#pragma pack()

#pragma pack(1)
typedef struct {

	NJS_INDICE_LIST_HEADER header;
	ImU8 chunk_type = 0;
	void* chunk_data;

} NJS_INDICE_LIST;
#pragma pack()

#pragma pack(1)
struct njcmModelStruct {

	NJS_VERTEX_LIST* vertex;
	NJS_INDICE_LIST* indice;
	NJS_VECTOR center = { 0.0f, 0.0f, 0.0f };
	float radius = 0.0f;

};
#pragma pack()

#pragma pack(1)
struct njcmObjectStruct {

	ImU32 eval_flags = 0;
  /*0x00000001 Don't apply translation, when x, y, z are 0
	0b00000010 Don't apply rotation, when rot_x, rot_y, rot_z are 0
	0b00000100 Don't apply scale, when scl_x, scl_y, scl_z are 0
	0b00001000 Don't render model
	0b00010000 Break child trace, don't render child objects
	0b00100000 ZXY rotation order, apply rotations in LightWave3D order
	0b01000000 Skip motion
	0b10000000 Skip shape motion*/
	njcmModelStruct* model;						//POF0 sets to offset from start of chunk(chunk_body)
	float position[3] = { 0.0f, 0.0f, 0.0f };
	ImU32 rotation[3] = { 0, 0, 0 };
	float scale[3] = { 0.0f, 0.0f, 0.0f };
	njcmObjectStruct* child_offset;						//POF0 sets to offset from start of chunk(chunk_body)
	njcmObjectStruct* sibling_offset;					//POF0 sets to offset from start of chunk(chunk_body)

};
#pragma pack()

#pragma pack(1)
struct njcmStruct {

	char chunk_type_id[4] = { 'N', 'J', 'C', 'M' };
	ImU32 chunk_body_size = 0;
	njcmObjectStruct chunk_body;
	POF0* chunk_pointer;

};
#pragma pack()