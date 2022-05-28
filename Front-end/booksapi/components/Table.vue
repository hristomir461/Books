<template>
  <v-card>
    <v-card-title>
      Nutrition
      <v-spacer></v-spacer>
      <v-text-field
        v-model="search"
        append-icon="mdi-magnify"
        label="Search"
        single-line
        hide-details
      ></v-text-field>
    </v-card-title>
    <v-data-table :headers="headers" :items="books" :search="search">
      <template v-slot:[`item.edit`]="{ item }">
        <v-btn color="success" @click="editItem(item)">Edit</v-btn>
      </template>
      <template v-slot:[`item.delete`]="{ item }">
        <v-btn color="warning" @click="deleteItem(item.id)">Delete</v-btn>
      </template>
    </v-data-table>
  </v-card>
</template>

<script>
export default {
  data() {
    return {
      search: "",
      headers: [
        { text: "Title", value: "title" },
        { text: "Author", value: "author" },
        { text: "Edition", value: "edition" },
        { text: "Genre", value: "genre" },
        { text: "Edit", value: "edit" },
        { text: "Delete", value: "delete" },
      ],
    }
  },
  computed:{
      books(){
          return this.$store.state.books.data;
      }
  },
  async fetch(){
      this.$store.commit(
          "books/storeData",
          (await this.$axios.get("http://books-api.somee.com/api/Books/")).data)
  },
  methods:{
     async deleteItem(id){
         await this.$axios.delete("http://books-api.somee.com/api/Books/" + id);
           this.$store.commit(
          "books/storeData",
          (await this.$axios.get("http://books-api.somee.com/api/Books/")).data);
      },
          async editItem(book){
           this.$store.commit("book/storeId", book.id);
           this.$store.commit("book/storeTitle", book.title);
           this.$store.commit("book/storeAuthor", book.author);
           this.$store.commit("book/storeEdition", book.edition);
           this.$store.commit("book/storeGenre", book.genre);
      }
  }
}
</script>

<style>
</style>