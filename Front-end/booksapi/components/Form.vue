<template>

  <v-form ref="form"
    v-model="valid"
    lazy-validation>
    <v-text-field v-if="id" v-model="id" label="Id"></v-text-field>
    <v-text-field :rules="[v => !!v || 'Title is required']" v-model="title" label="Title" required></v-text-field>
    <v-text-field :rules="[v => !!v || 'Author is required']" v-model="author" label="Author" required></v-text-field>
    <v-date-picker v-model="edition" label="Edition" required></v-date-picker>
    <v-text-field  :rules="[v => !!v || 'Genre is required']" v-model="genre" label="Genre" required></v-text-field>
    <v-btn :disabled="!valid"  @click="submit({id, title, author, edition, genre})" >{{id ? "Edit" : "Submit"}}</v-btn>
  </v-form>
</template>

<script>
export default {
    data: () => ({
    valid: true,
    }),
  computed: {
    id: {
      get() {
        return this.$store.state.book.id;
      },
      set(value) {
        this.$store.commit("book/storeId", value);
      },
    },
    title: {
      get() {
        return this.$store.state.book.title;
      },
      set(value) {
        this.$store.commit("book/storeTitle", value);
      },
    },
    author: {
      get() {
        return this.$store.state.book.author;
      },
      set(value) {
        this.$store.commit("book/storeAuthor", value);
      },
    },
    edition: {
      get() {
       return this.$store.state.book.edition;
      },
      set(value) {
        this.$store.commit("book/storeEdition", value);
      },
    },
    genre: {
      get() {
        return this.$store.state.book.genre;
      },
      set(value) {
        this.$store.commit("book/storeGenre", value);
      },
    },
  },
  methods: {
     async submit(book){
         if(this.$refs.form.validate()){
            if(book.id){
              await this.$axios.put("http://books-api.somee.com/api/Books?id="+ book.id, {
                id: book.id,
                title: book.title,
                author: book.author,
                edition: book.edition,
                genre: book.genre
              });
          }else{
              await this.$axios.post("http://books-api.somee.com/api/Books/", {
                title: book.title,
                author: book.author,
                edition: book.edition,
                genre: book.genre
              });
          }
          await this.resetForm({id:0,title:"",author:"",edition:(new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),genre:""});
           this.$store.commit(
          "books/storeData",
          (await this.$axios.get("http://books-api.somee.com/api/Books")).data);
          this.$refs.form.resetValidation()
         }
      },
        resetForm(book){
           this.$store.commit("book/storeId", book.id);
           this.$store.commit("book/storeTitle", book.title);
           this.$store.commit("book/storeAuthor", book.author);
           this.$store.commit("book/storeEdition", book.edition);
           this.$store.commit("book/storeGenre", book.genre);
      }
  },
};
</script>